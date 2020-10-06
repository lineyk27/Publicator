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

namespace Publicator.Core.Domains.User.Commands
{
    class LogInHandler : IRequestHandler<LogIn, LoginResult>
    {
        private readonly PublicatorDbContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly ILogger<LogInHandler> _logger;
        private readonly UserManager<Infrastructure.Models.User> _userManager;
        private readonly SignInManager<Infrastructure.Models.User> _signInManager;
        public LogInHandler(
            PublicatorDbContext context, 
            IOptions<JWTSettings> options, 
            ILogger<LogInHandler> logger,
            UserManager<Infrastructure.Models.User> userManager,
            SignInManager<Infrastructure.Models.User> signInManager
            )
        {
            _context = context;
            _jwtSettings = options.Value;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<LoginResult> Handle(LogIn request, CancellationToken cancellationToken)
        {
            var result = new LoginResult();
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
                result.Token = GetAuthToken(user);
                return result;
            }
            else
            {
                result.Result = LoginResultEnum.BadCredentials;
                return result;
            }
        }
        private string GetAuthToken(Infrastructure.Models.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                }),
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokendescriptor);
            var tokenkey = tokenHandler.WriteToken(token);
            
            return tokenkey;
        }
        private bool CheckPassword(string password, Infrastructure.Models.User user)
        {
            using SHA256 algo = SHA256.Create();
            algo.Initialize();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashed = algo.ComputeHash(bytes);

            StringBuilder hashbuilder = new StringBuilder(hashed.Length);
            foreach (var i in hashed)
            {
                hashbuilder.Append(i.ToString("x2"));
            }

            return hashbuilder.ToString().Equals(user.PasswordHash);
        }
    }
}
