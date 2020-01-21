namespace Publicator.ApplicationCore.Contracts
{
    public interface IEmailService
    {
        public void SendEmailAsync(string email, string subject, string text);
    }
}
