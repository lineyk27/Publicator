using MediatR;

namespace Publicator.Core.Domains.User.Commands
{
    public class FacebookLogIn : IRequest<LogInResult>
    {
        public string AccessToken { get; set; }
    }
}
