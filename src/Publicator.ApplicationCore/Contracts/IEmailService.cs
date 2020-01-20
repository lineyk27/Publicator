namespace Publicator.ApplicationCore.Contracts
{
    interface IEmailService
    {
        public void SendEmailAsync(string email, string subject, string text);
    }
}
