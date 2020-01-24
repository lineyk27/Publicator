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
    /// <summary>
    /// Controller for authentication and authorization process
    /// </summary>
    public class AccountController : Controller
    {
        private IUserService _userService;
        private JWTSettings _jwtSettings;
        private IMapper _mapper;
        public AccountController(IUserService userService, IOptions<JWTSettings> options, IMapper mapper)
        {
            _userService = userService;
            _jwtSettings = options.Value;
            _mapper = mapper;
        }
        /// <summary>
        /// Logim method, authenticate user
        /// </summary>
        /// <param name="model">Model with login and password fields</param>
        /// <returns>If auth succesfull, return auth token, either - error</returns>
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
        /// <summary>
        /// Register user method
        /// </summary>
        /// <param name="model">Model with user information needed for registration process</param>
        /// <returns>If register succesfull then OkResult, either error</returns>
        // POST: /register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Password != model.ConfirmPassword)
                return BadRequest(ModelState);

            await _userService.RegisterAsync(model.Username, model.Email, model.Password);

            return Ok();
        }
        /// <summary>
        /// Method for confirm account by email
        /// </summary>
        /// <param name="id">Id of user to confirm</param>
        /// <param name="token">Token for user to confirm</param>
        /// <returns>If user confirmed - true, else false of erroro if user not found</returns>
        // GET: /confirm?id=231..54&token=351..35
        [Route("confirm")]
        public async Task<IActionResult> ConfirmAccount([FromQuery] Guid id, [FromQuery]string token)
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
        /// <summary>
        /// Method to return current authenticated user
        /// </summary>
        /// <returns>UserDTO of authenricated user</returns>
        // GET: /api/current
        [Authorize]
        [Route("api/current")]
        public async Task<IActionResult> CurrentUser()
        {
            var user = await _userService.GetCurrentUserAsync();
            var dto = _mapper.Map<User, UserDTO>(user);
            return Ok(dto);
        }
    }
}