using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Publicator.ApplicationCore.Contracts;
using Publicator.Presentation.Helpers;
using Publicator.Presentation.RequestModels;

namespace Publicator.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private JWTSettings _jwtSettings;
        public AccountController(IUserService userService, IOptions<JWTSettings> options)
        {
            _userService = userService;
            _jwtSettings = options.Value;
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest model)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var user = await _userService.LoginAsync(model.Login, model.Password);

            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nickname),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                }),
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokendescriptor);
            var tokenkey = tokenhandler.WriteToken(token);

            return Ok(tokenkey);
        }
    }
}