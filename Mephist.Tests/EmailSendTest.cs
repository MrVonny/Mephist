using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Xunit;

namespace Mephist.Tests
{
    public class EmailSendTest
    {
        [Fact]
        public void Test1()
        {
            

            var body = new BodyBuilder()
            {
                HtmlBody = $"Подтвердите регистрацию, перейдя по ссылке: <a href='abobus.com'>link</a>"
            };

            MimeMessage message = new MimeMessage()
            {
                Subject = "Подтвердите свою личность",
                Body = body.ToMessageBody(),
                From = { new MailboxAddress("Admin", "info@mephist2.ru") },
                To = { new MailboxAddress("", "borafon@gmail.com") }
            };
            Exception exception = null;
            try
            {
                using var client = new HttpClient(new HttpClientHandler());

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("authorization", "lfl131s2V3hl4u0TkYcSzB5DTZIqC3iO8tE1");

                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("from","info@mephist2.ru"),
                    new KeyValuePair<string,string>("name","Mephist2"),
                    new KeyValuePair<string,string>("to","t33maks33@yandex.ru"),
                    new KeyValuePair<string,string>("subject","Test"),
                    new KeyValuePair<string,string>("html",$"Подтвердите регистрацию, перейдя по ссылке: <a href='abobus.com'>link</a>")
                });
                var response =  client.PostAsync(@"https://api.smtp.bz/v1/smtp/send", content).Result;

            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.True(exception == null); 
        }
    }
}
