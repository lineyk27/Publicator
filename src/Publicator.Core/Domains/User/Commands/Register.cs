using System;
using MediatR;

namespace Publicator.Core.Domains.User.Commands
{
    public class Register : IRequest<RegisterResult>
    {
        public string Email { get; set; }
        public string Nickname{ get; set; }
        public string Password{ get; set; }
        public string ConfirmPassword{ get; set; }
    }
}
