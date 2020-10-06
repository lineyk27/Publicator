using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace Publicator.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration) => _configuration = configuration;
        public void SendConfirmationEmail(string email, string token, string username)
        {
            var emailUsername = _configuration["Email:Gmail:Username"];
            var emailPassword = _configuration["Email:Gmail:Password"];
            var emailEmail= _configuration["Email:Gmail:Email"];

            var message = new MimeMessage();
            
            message.To.Add(new MailboxAddress(emailUsername, email));
            message.From.Add(new MailboxAddress("Admin", emailEmail));
            token = HttpUtility.UrlEncode(token);
            email = HttpUtility.UrlEncode(email);
            
            message.Subject = "Admin";

            message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = String.Format("{0}, please confirm your Email,\n" +
                "confirm link: https://localhost:5001/confirmEmail?email={1}&token={2}",
                username, email, token)
            };

            using (var client = new SmtpClient())
            {
                
                client.Connect("smtp.gmail.com", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(emailUsername, emailPassword);
                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}
