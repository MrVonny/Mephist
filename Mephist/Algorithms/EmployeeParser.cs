using HtmlAgilityPack;
using Mephist.Data;
using Mephist.Extensions;
using Mephist.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HeyRed.Mime;

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
            public static readonly string Photo = @"//img[@class='user-responsive']";
        }

        private static readonly string letters = "А";//"АБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ";
        private static readonly string path = "https://home.mephi.ru";

        private readonly IWebHostEnvironment _webHost;
        public EmployeeParser(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public  List<Employee> GetEmployees()
        {
            return GetEmployees(letters.ToArray());

        }
        public List<Employee> GetEmployees(char[] firstLetters)
        {
            List<Employee> employees = new List<Employee>();
            Parallel.ForEach(GetLinks(firstLetters), (link) =>
            {
                var emp = ParseEmployee(link);
                lock (link)
                    employees.Add(emp);
            });
            return employees;
        }
        private Employee ParseEmployee(string link)
        {
            using(WebClient webClient = new WebClient())
            {
                HtmlDocument html = new HtmlDocument();

                html.LoadHtml(webClient.DownloadString(link));
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
                        string sub = matches.Groups[1].Value;
                        subjects.Add(NormalizeSubject(sub));
                        matches = matches.NextMatch();
                    }

                    employee.Subjects = subjects;

                }

                //Кафедры
                var departmentsNode = html.DocumentNode.SelectSingleNode(XPaths.DeparmentAndIstitute);
                if (departmentsNode != null)
                {
                    string str = departmentsNode.InnerText.ToString();
                    var list = str.Split(" / ");
                    employee.Departments = list.ToList();
                }

                //Фотография
                var photoNode = html.DocumentNode.SelectSingleNode(XPaths.Photo);
                if(photoNode != null)
                {
                    string url = path + photoNode.Attributes["src"].Value.Replace("&#39;","'");
                    string extension = url.Split('.').Last().Split('?').First();
                    if (!extension.Equals("svg"))
                    {
                        string savePath = $"Content/Employees/{employee.FullName.Transliterate(true)}";
                        var name = url.Split('/').Last().Split('.').First().Transliterate(true);
                        name = $"Avatar_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.{extension}";
                        Directory.CreateDirectory(Path.Combine(_webHost.WebRootPath, savePath));
                        webClient.DownloadFile(url, Path.Combine(_webHost.WebRootPath, savePath, name));

                        Media photo = new Media()
                        {
                            PartialMediaPath = savePath,
                            MediaName = name,
                            CreatedDate = DateTime.Now,
                            ContentType = MimeTypesMap.GetMimeType(name),
                        };

                        employee.Photos = new List<Media> { photo };

                    }
                   
                }

                return employee;
            }
        }
        private List<string> GetLinks()
        {
            return GetLinks(letters.ToArray());
        }
        private List<string> GetLinks(char[] letters)
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
                    html.LoadHtml(webClient.DownloadString(path + query));
                    HtmlNodeCollection linkBlock = html.DocumentNode.SelectNodes(XPaths.LinkBlock);
                    if (linkBlock == null)
                        break;
                    links.AddRange(linkBlock.Select(x => path + x.Attributes["href"].Value));
                } while (true);

            }

            return links;
        }
        public static string NormalizeSubject(string sub)
        {
            void RemoveRegex(StringBuilder builder, string regExp)
            {
                var match = Regex.Match(builder.ToString(), regExp);
                while (match.Success)
                {
                    int index = match.Index;
                    builder = builder.Remove(index, match.Value.Length);
                    match = Regex.Match(builder.ToString(), regExp);
                } 
            }

            void ReplaceRegex(StringBuilder builder, string regExp, string replaceStr)
            {
                var match = Regex.Match(builder.ToString(), regExp);
                while (match.Success)
                {
                    int index = match.Index;
                    builder = builder.Replace(match.Value, replaceStr,0 , builder.Length);
                    match = Regex.Match(builder.ToString(), regExp);
                } 
            }

            StringBuilder builder = new StringBuilder(sub);

            //Лишние пробелы           
            RemoveRegex(builder, @"[\f\n\r\t\v]");
            ReplaceRegex(builder, @"\s{2,}", " ");
            RemoveRegex(builder, @"^\s");
            RemoveRegex(builder, @"\s$");


            //Заглваная буква
            char firstLetter = builder[0];
            if (Char.IsLower(firstLetter))
                builder[0] = Char.ToUpper(firstLetter);

            //Лишние пробелы возле скобокок
            ReplaceRegex(builder,@"\(\s", "(");
            ReplaceRegex(builder,@"\s\)", ")");


            //Недостующие пробелы возле скобок
            var match = Regex.Match(builder.ToString(), @"\w\p{Ps}");
            while (match.Success)
            {
                int index = match.Index;
                builder = builder.Insert(index + 1, ' ');

                match = match.NextMatch();
            }
            
            


            return builder.ToString();
        }
    }
}
