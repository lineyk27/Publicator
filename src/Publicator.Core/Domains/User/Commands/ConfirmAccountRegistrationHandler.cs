using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Commands
{
    class ConfirmAccountRegistrationHandler :
        IRequestHandler<ConfirmAccountRegistration, RegistrationConfirmationResult>
    {
        private readonly PublicatorDbContext _context;
        public ConfirmAccountRegistrationHandler(PublicatorDbContext context)
        {
            _context = context;
        }
        public async Task<RegistrationConfirmationResult> Handle(
            ConfirmAccountRegistration request, 
            CancellationToken cancellationToken
            )
        {
            var result = new RegistrationConfirmationResult();

            var user = (from u in _context.Users
                        where u.Id.Equals(request.UserId)
                        select u)
                        .FirstOrDefault();

            if (user == null)
                result.Result = RegistrationConfirmationEnum.BadConfirmation;

            if (user.EmailConfirmed)
                result.Result = RegistrationConfirmationEnum.AlreadyConfirmed;
            else
            {
                result.Result = RegistrationConfirmationEnum.ConfirmationSuccesfull;
                user.EmailConfirmed = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (!request.ConfirmationToken.Equals(request.UserId))
                result.Result = RegistrationConfirmationEnum.BadConfirmation;

            return result;
        }
    }
}
