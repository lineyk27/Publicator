namespace Publicator.ApplicationCore.Contracts
{
    interface IEmailService
    {
        public void SendEmail(string email, string subject, string text);
    }
}
