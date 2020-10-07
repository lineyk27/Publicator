using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Publicator.Core.Helpers;
using Publicator.Core.Exceptions;
using Publicator.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Publicator.Core.Services;

namespace Publicator.Core.Domains.User.Commands
{
    class LogInHandler : IRequestHandler<LogIn, LogInResult>
    {
        private readonly PublicatorDbContext _context;
        private readonly ILogger<LogInHandler> _logger;
        private readonly UserManager<Infrastructure.Models.User> _userManager;
        private readonly SignInManager<Infrastructure.Models.User> _signInManager;
        private readonly ITokenService _tokenService;
        public LogInHandler(
            PublicatorDbContext context, 
            ILogger<LogInHandler> logger,
            ITokenService tokenService,
            UserManager<Infrastructure.Models.User> userManager,
            SignInManager<Infrastructure.Models.User> signInManager
            )
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<LogInResult> Handle(LogIn request, CancellationToken cancellationToken)
        {
            var result = new LogInResult();
            var user = await _userManager.FindByEmailAsync(request.Login);

            if(!user.EmailConfirmed)
            {
                result.Result = LoginResultEnum.IsNotConfirmed;
                return result;
            }

            var signinRes = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

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
