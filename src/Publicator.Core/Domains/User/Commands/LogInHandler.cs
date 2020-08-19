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

namespace Publicator.Core.Domains.User.Commands
{
    class LogInHandler : IRequestHandler<LogIn, string>
    {
        private readonly PublicatorDbContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly ILogger<LogInHandler> _logger;
        public LogInHandler(
            PublicatorDbContext context, 
            IOptions<JWTSettings> options, 
            ILogger<LogInHandler> logger
            )
        {
            _context = context;
            _jwtSettings = options.Value;
            _logger = logger;
        }
        public async Task<string> Handle(LogIn request, CancellationToken cancellationToken)
        {
            var user = (from u in _context.Users.Include(x => x.Role)
                        where (u.Email.Equals(request.Login) || 
                               u.Nickname.Equals(request.Login)
                               ) &&  u.EmailConfirmed
                        select u
                        ).FirstOrDefault();

            if(user != null)
            {
                var isPasswordGood = CheckPassword(request.Password, user);
                if (isPasswordGood)
                {
                    _logger.LogInformation("Succesfull acthentication");
                    var tokenKey = GetAuthToken(user);
                    return await Task.FromResult(tokenKey);
                }
            }
            _logger.LogInformation("Wrong authentification for user");
            throw new FailedAuthenticationException();
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
                    new Claim(ClaimTypes.Name, user.Nickname),
                    new Claim(ClaimTypes.Role, user.Role.Name)
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
