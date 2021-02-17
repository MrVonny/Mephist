using HtmlAgilityPack;
using Mephist.Data;
using Mephist.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mephist.Algorithms
{
    
    public class EmployeeParser
    {
        public static class XPaths
        {
            public static readonly string FullName = @"//h1[@class='text-center no-margin-top']";
            public static readonly string Positions = @"//h1[@class='text-center no-margin-top']/following-sibling::div[@class='list-group']/div/h4/text()[1]";
            public static readonly string DeparmentAndIstitute = @"//h1[@class='text-center no-margin-top']/following-sibling::div[@class='list-group']/div/div";
            public static readonly string Subjects = @"//h3[text()='Преподаваемые дисциплины']/following::div[1]";
            public static readonly string LinkBlock = @"//a[@class='list-group-item list-group-item-user-public']";
        }

        private readonly string letters = "АБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ";
        private readonly string path = "https://home.mephi.ru";

        
        public EmployeeParser()
        {
            
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            foreach (var link in await GetLinks())
            {
                employees.Add(await ParseEmployee(link));               
            }
            return employees;

        }

        public async Task<List<Employee>> GetEmployees(char[] firstLetters)
        {
            List<Employee> employees = new List<Employee>();
            foreach (var link in await GetLinks(firstLetters))
            {
                employees.Add(await ParseEmployee(link));
            }
            return employees;

        }

        private static async Task<Employee> ParseEmployee(string link)
        {
            HtmlDocument html = new HtmlDocument();
            WebClient webClient = new WebClient();
            html.LoadHtml(await webClient.DownloadStringTaskAsync(link));
            Employee employee = new Employee();

            //ФИО
            string fullName = html.DocumentNode.SelectSingleNode(XPaths.FullName).InnerText;
            employee.FullName = fullName;

            //Должности
            var positionNodes = html.DocumentNode.SelectNodes(XPaths.Positions);
            if (positionNodes != null)
            {
                var positions = positionNodes.Select(
                        x => new String(x.InnerText.Where(l => Char.IsLetter(l) || l.Equals(' ')).ToArray())
                    ).ToList();
         
                employee.Positions = positions;
            }

            //Предметы
            var subjectsNode = html.DocumentNode.SelectSingleNode(XPaths.Subjects);
            string dirtySubjects;
            if (subjectsNode != null)
            {
                dirtySubjects = subjectsNode.InnerText;

                List<string> subjects = new List<string>();

                Regex regex = new Regex(@"\d*\.\n([^\n]*)\n");
                Match matches = regex.Match(dirtySubjects);
                while (matches.Success)
                {
                    subjects.Add(matches.Groups[1].Value);
                    matches = matches.NextMatch();
                }

                employee.Subjects = subjects;

            }

            //Кафедры
            var departmentsNode = html.DocumentNode.SelectSingleNode(XPaths.DeparmentAndIstitute);
            if(departmentsNode != null)
            {
                employee.Departments = departmentsNode.InnerText.Split(@" / ").ToList();               
            }
            
            return employee;

        }

        private async Task<List<string>> GetLinks()
        {
            HtmlDocument html = new HtmlDocument();
            WebClient webClient = new WebClient();
            List<string> links = new List<string>();

            foreach (var letter in letters)
            {
                int page = 1;
                do
                {
                    string query = $@"/ru/people?char={letter}&page={page++}";
                    html.LoadHtml(await webClient.DownloadStringTaskAsync(path + query));
                    HtmlNodeCollection linkBlock = html.DocumentNode.SelectNodes(XPaths.LinkBlock);
                    if (linkBlock == null)
                        break;
                    links.AddRange(linkBlock.Select(x => path + x.Attributes["href"].Value));
                } while (true);

            }

            return links;
        }

        private async Task<List<string>> GetLinks(char[] letters)
        {
            HtmlDocument html = new HtmlDocument();
            WebClient webClient = new WebClient();
            List<string> links = new List<string>();

            foreach (var letter in letters)
            {
                int page = 1;
                do
                {
                    string query = $@"/ru/people?char={letter}&page={page++}";
                    html.LoadHtml(await webClient.DownloadStringTaskAsync(path + query));
                    HtmlNodeCollection linkBlock = html.DocumentNode.SelectNodes(XPaths.LinkBlock);
                    if (linkBlock == null)
                        break;
                    links.AddRange(linkBlock.Select(x => path + x.Attributes["href"].Value));
                } while (true);

            }

            return links;
        }
    }
}
