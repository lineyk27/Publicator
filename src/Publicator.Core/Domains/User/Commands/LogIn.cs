using MediatR;

namespace Publicator.Core.Domains.User.Commands
{
    public class LogIn : IRequest<string>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
