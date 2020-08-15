using System;
using MediatR;

namespace Publicator.Core.Domains.User.Commands
{
    public class ConfirmAccountRegistration : IRequest<RegistrationConfirmationResult>
    {
        public Guid UserId { get; set; }
        public string ConfirmationToken { get; set; }
    }
}
