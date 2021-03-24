using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mephist.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            
            var emailMessage = new MailMessage();
            emailMessage.IsBodyHtml = true;
            emailMessage.From = new MailAddress("admin@mephist2.com");
            emailMessage.To.Add(new MailAddress(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            }.Text;
            /*
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mailtrap.io", 2525, true);
                await client.AuthenticateAsync("newmephist@gmail.com", "NewMephist2020");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            */
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("108cafddb3d645", "b7d219e0136730"),
                EnableSsl = true
            };
            //client.Send("from@example.com", "to@example.com", "Hello world", "testbody");
            client.Send(emailMessage);
        }
    }
}
