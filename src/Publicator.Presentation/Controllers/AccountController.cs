using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Presentation.Helpers;
using Publicator.Presentation.RequestModels;
using Publicator.Infrastructure.Entities;

namespace Publicator.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private JWTSettings _jwtSettings;
        private IMapper _mapper;
        public AccountController(IUserService userService, IOptions<JWTSettings> options,IMapper mapper)
        {
            _userService = userService;
            _jwtSettings = options.Value;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
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
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (model.Password != model.ConfirmPassword)
                return BadRequest(ModelState);

            await _userService.RegisterAsync(model.Username, model.Email, model.Password);
            
            return Ok();
        }
        [Route("/confirm?userid={id}&token={token}")]
        public async Task<IActionResult> ConfirmAccount([FromRoute]Guid id, [FromRoute]string token)
        {
            User user;
            try
            {
                user = await _userService.GetByIdAsync(id);
            }
            catch
            {
                return StatusCode(400);
            }
            bool result = _userService.ConfirmAccount(user, token);

            return Ok(new { Confirmed = result });
        }
        [Authorize()]
        [Route("api/current")]
        public async Task<IActionResult> CurrentUser()
        {
            var user = await _userService.GetByUsernameAsync(HttpContext.User.Identity.Name);
            var dto = _mapper.Map<User, UserDTO>(user);
            return Ok(dto);
        }
    }
}