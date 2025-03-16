using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Publicator.Core.Services;
using Publicator.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Publicator.Core.Domains.User.Commands
{
    class LogInHandler : IRequestHandler<LogIn, LogInResult>
    {
        private readonly ILogger<LogInHandler> _logger;
        private readonly UserManager<Infrastructure.Models.User> _userManager;
        private readonly SignInManager<Infrastructure.Models.User> _signInManager;
        private readonly ITokenService _tokenService;

        public LogInHandler(
            ILogger<LogInHandler> logger,
            ITokenService tokenService,
            UserManager<Infrastructure.Models.User> userManager,
            SignInManager<Infrastructure.Models.User> signInManager
            )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<LogInResult> Handle(LogIn request, CancellationToken cancellationToken)
        {
            var result = new LogInResult();

            var user = await _userManager.FindByEmailAsync(request.Login);

            if (user is null)
            {
                result.Result = LoginResultEnum.BadCredentials;
                return result;
            }

            if (!user.EmailConfirmed)
            {
                result.Result = LoginResultEnum.IsNotConfirmed;
                return result;
            }

            var signinRes = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (signinRes.Succeeded)
            {
                result.Result = LoginResultEnum.Succesfull;
                result.Token = _tokenService.GenerateToken(user);
                return result;
            }
            else
            {
                result.Result = LoginResultEnum.BadCredentials;
                return result;
            }
        }
    }
}
