using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Core.Domains.User.Commands;
using Publicator.Core.Domains.User.Queries;
using Publicator.Presentation.RequestModels;
using Publicator.Infrastructure.Models;

namespace Publicator.Presentation.Controllers
{
    /// <summary>
    /// Controller for authentication and authorization process
    /// </summary>
    public class AccountController : BaseController
    {
        private IUserService _userService;
        private IMapper _mapper;
        private IMediator _mediator;
        public AccountController(IUserService userService,
            IMapper mapper,
            IMediator mediator
            ) 
            : base()
        {
            _userService = userService;
            _mapper = mapper;
            _mediator = mediator;
        }
        /// <summary>
        /// Logim method, authenticate user
        /// </summary>
        /// <param name="model">Model with login and password fields</param>
        /// <returns>If auth succesfull, return auth token, either - error</returns>
        // POST: api/account/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var tokenKey = await _mediator.Send(new LogIn()
            {
                Login = model.Login,
                Password = model.Password
            });
            
            return Ok(tokenKey);
        }
        /// <summary>
        /// Register user method
        /// </summary>
        /// <param name="model">Model with user information needed for registration process</param>
        /// <returns>If register succesfull then OkResult, either error</returns>
        // POST: api/account/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send<RegisterResult>(new Register()
            {
                Email = model.Email,
                Nickname = model.Username,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            });

            return Ok(result);
        }
        /// <summary>
        /// Method for confirm account by email
        /// </summary>
        /// <param name="id">Id of user to confirm</param>
        /// <param name="token">Token for user to confirm</param>
        /// <returns>If user confirmed - true, else false of erroro if user not found</returns>
        // GET: api/account/confirm?id=231..54&token=351..35
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
        /// <returns>UserDTO of authenticated user</returns>
        // GET: /api/account/current
        [Authorize]
        [Route("current")]
        public async Task<IActionResult> CurrentUser()
        {
            var user = await _mediator.Send<User>(new LoggedInUser());

            var dto = _mapper.Map<User, UserDTO>(user);
            return Ok(dto);
        }
        /// <summary>
        /// Change user pic
        /// </summary>
        /// <param name="model">Model with image url</param>
        /// <returns>Ok if all ok</returns>
        // PUT: api/account/picture
        [HttpPut]
        [Authorize]
        [Route("picture")]
        public IActionResult ChangeUserPicture(ChangePictureRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.ChangeUserPicture(model.Url);
            return Ok();
        }
    }
}