using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.ApplicationCore.Helpers;

namespace Publicator.ApplicationCore.Services
{
    class EmailService : IEmailService
    {
        EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public async void SendEmailAsync(string email, string subject, string text)
        {
            var senderEmail = _emailSettings.Email;
            var senderPassword = _emailSettings.Password;

            MailMessage mail = new MailMessage();
            using SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(senderEmail);
            mail.To.Add(email);
            mail.Subject = "Email from Publicator";
            mail.Body = text;

            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;
            try
            {
                await smtpClient.SendMailAsync(mail);
            }
            catch (InvalidOperationException e)
            {
                throw new ResourceException(e.Message);
            }
        }
    }
}
