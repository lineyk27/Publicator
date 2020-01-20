using System.Net;
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
            MailMessage mail = new MailMessage();
            // TODO: add more info for send email
            SmtpClient smtpClient = new SmtpClient("");
            mail.From = new MailAddress("");
            mail.To.Add(email);
            mail.Subject = "Email from Publicator";
            mail.Body = text;

            smtpClient.Port = 587;
            // TODO: add password and email, or any config to get it
            smtpClient.Credentials = new NetworkCredential("", "");
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mail);
        }
    }
}
