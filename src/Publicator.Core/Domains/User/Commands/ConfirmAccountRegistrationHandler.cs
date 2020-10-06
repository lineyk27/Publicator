using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Commands
{
    class ConfirmAccountRegistrationHandler :
        IRequestHandler<ConfirmAccountRegistration, RegistrationConfirmationResult>
    {
        private readonly PublicatorDbContext _context;
        private readonly ILogger<ConfirmAccountRegistrationHandler> _logger;
        private readonly UserManager<Infrastructure.Models.User> _userManager;
        public ConfirmAccountRegistrationHandler(
            PublicatorDbContext context, 
            ILogger<ConfirmAccountRegistrationHandler> logger,
            UserManager<Infrastructure.Models.User> userManager
            )
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<RegistrationConfirmationResult> Handle(
            ConfirmAccountRegistration request, 
            CancellationToken cancellationToken
            )
        {
            var result = new RegistrationConfirmationResult();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                if (user.EmailConfirmed == true)
                {
                    result.Result = RegistrationConfirmationEnum.AlreadyConfirmed;
                    _logger.LogInformation("An attempt to confirm already confirmed account by email: {0}", request.Email);
                    return result;
                }

                var confirmResult = await _userManager.ConfirmEmailAsync(user, request.Token);

                if (confirmResult.Succeeded)
                {
                    result.Result = RegistrationConfirmationEnum.ConfirmationSuccesfull;
                    _logger.LogInformation("Confirmation of email was succesfull for email: {0}", request.Email);
                    return result;
                }
                else
                {
                    result.Result = RegistrationConfirmationEnum.BadConfirmation;
                    _logger.LogWarning(confirmResult.Errors.FirstOrDefault().Description);
                    return result;
                }
            }
            result.Result = RegistrationConfirmationEnum.BadConfirmation;
            return result;
        }
    }
}
