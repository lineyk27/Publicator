using System;
using MediatR;

namespace Publicator.Core.Domains.User.Commands
{
    public class ConfirmAccountRegistration : IRequest<RegistrationConfirmationResult>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
