using MediatR;

namespace Publicator.Core.Domains.User.Commands
{
    public class LogIn : IRequest<LoginResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
