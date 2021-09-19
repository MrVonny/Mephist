using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


namespace Mephist.Services
{
    public class EmailSender
    {
        private readonly IWebHostEnvironment _environment;

        public EmailSender(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task SendConfirmEmail(string email, string callbackUrl)
        {
            
            using var client = new HttpClient(new HttpClientHandler());

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("authorization", "lfl131s2V3hl4u0TkYcSzB5DTZIqC3iO8tE1");
            //client.DefaultRequestHeaders.Add("content-type", "multipart/form-data");

            var html = await File.ReadAllTextAsync(Path.Combine(_environment.WebRootPath,
                "Content/Shared/EmailConfirm.html"));

            html = html.Replace("[LINK]", callbackUrl);
            html = html.Replace("[EMAIL]", email);


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", "info@mephist2.ru"),
                new KeyValuePair<string, string>("name", "Mephist2.ru"),
                new KeyValuePair<string, string>("to", email),
                new KeyValuePair<string, string>("subject", "Подтвердите свой аккаунт"),
                new KeyValuePair<string, string>("html", html)
            });
            var response = await client.PostAsync(@"https://api.smtp.bz/v1/smtp/send", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

        }
    }
}
