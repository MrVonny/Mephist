using HtmlAgilityPack;
using Mephist.Data;
using Mephist.Extensions;
using Mephist.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HeyRed.Mime;
using Mephist.Services;

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

        private static readonly string letters = "АБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ";
        private static readonly string path = "https://home.mephi.ru";

        private readonly IWebHostEnvironment _webHost;
        public EmployeeParser(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            ConcurrentBag<Employee> employees = new ConcurrentBag<Employee>();
            await Task.Run(() => Parallel.ForEach(GetLinks(letters.ToCharArray()),new ParallelOptions()
            {
                MaxDegreeOfParallelism = 5
            }, link =>
            {
                var task = ParseEmployee(link);
                task.Wait();
                if (task.IsCompletedSuccessfully)
                    employees.Add(task.Result);
                else
                {
                    var e = task.Exception ?? new AggregateException();
                    Console.WriteLine(e.Message);
                    throw e;
                }
                    
            }));
            
            return employees.ToList();

        }
        public async Task<List<Employee>> GetEmployees(char[] firstLetters)
        {
            return (await Task.WhenAll(GetLinks(firstLetters).Select(async x => await ParseEmployee(x)))).ToList();
        }
        private async Task<Employee> ParseEmployee(string link)
        {
            using WebClient webClient = new WebClient();
            HtmlDocument html = new HtmlDocument();

            html.LoadHtml(await webClient.DownloadStringTaskAsync(link));
            Employee employee = new Employee();

            //ФИО
            string fullName = html.DocumentNode.SelectSingleNode(XPaths.FullName).InnerText;
            employee.FullName = fullName;
            Console.WriteLine($"Starting parsing: {fullName}");
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
                    var name = $"Avatar_{employee.FullName.Transliterate(true)}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.{extension}";
                    var bytes = await webClient.DownloadDataTaskAsync(url);
                    AwsS3Storage storage = new AwsS3Storage();
                    Media photo = new Media()
                    {
                        Key = name,
                        MediaName = name,
                        CreatedDate = DateTime.Now,
                        ContentType = MimeTypesMap.GetMimeType($"_.{extension}"),
                    };
                    
                    await storage.AddItem(bytes, photo.Key, MimeTypesMap.GetMimeType($"_.{extension}"));

                    employee.Photos = new List<Media> { photo };

                }
                   
            }

            return employee;
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
