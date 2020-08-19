using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Commands
{
    class ConfirmAccountRegistrationHandler :
        IRequestHandler<ConfirmAccountRegistration, RegistrationConfirmationResult>
    {
        private readonly PublicatorDbContext _context;
        private readonly ILogger<ConfirmAccountRegistrationHandler> _logger;
        public ConfirmAccountRegistrationHandler(
            PublicatorDbContext context, 
            ILogger<ConfirmAccountRegistrationHandler> logger
            )
        {
            _context = context;
            _logger = logger;
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
            {
                result.Result = RegistrationConfirmationEnum.BadConfirmation;
                _logger.LogWarning("User to confirm account was not found by id: {}", request.UserId);
            }
            if (user.EmailConfirmed)
            {
                result.Result = RegistrationConfirmationEnum.AlreadyConfirmed;
                _logger.LogWarning("User's account was already confirmed by id: {}", request.UserId);
            }
            else
            {
                result.Result = RegistrationConfirmationEnum.ConfirmationSuccesfull;
                user.EmailConfirmed = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogError("User's account confirmation succesfull");

            }
            if (!request.ConfirmationToken.Equals(request.UserId))
            {
                result.Result = RegistrationConfirmationEnum.BadConfirmation;
                _logger.LogWarning("Bad confirmation token: {}", request.ConfirmationToken);
            }

            return result;
        }
    }
}
