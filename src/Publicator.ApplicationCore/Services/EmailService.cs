using System.Net;
using System.Linq;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Publicator.ApplicationCore.Contracts;

namespace Publicator.ApplicationCore.Services
{
    class EmailService : IEmailService
    {
        IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async void SendEmailAsync(string email, string subject, string text)
        {
            var configs = _configuration.GetSection("EmailSettings").GetChildren();
            var senderemail = configs.First(x => x.Key == "Email").Value;
            var senderpassword = configs.First(x => x.Key == "Password").Value;

            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(senderemail);
            mail.To.Add(email);
            mail.Subject = "Email from Publicator";
            mail.Body = text;

            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(senderemail, senderpassword);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mail);
        }
    }
}
