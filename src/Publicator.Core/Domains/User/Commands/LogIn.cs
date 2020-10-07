using MediatR;

namespace Publicator.Core.Domains.User.Commands
{
    public class LogIn : IRequest<LogInResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
