using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;
using Publicator.Presentation.RequestModels;
using Publicator.Presentation.ResponseModels;

namespace Publicator.Presentation.Controllers.Api
{
    public class UsersController : BaseController
    {
        IMapper _mapper;
        IUserService _userService;
        IPostService _postService;
        public UsersController(IMapper mapper,
            IUserService userService,
            IPostService postService)
        {
            _mapper = mapper;
            _userService = userService;
            _postService = postService;
        }
        /// <summary>
        /// Gets user by username
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <returns>User found by username</returns>
        // GET: api/users/john03
        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> GetByUsername([FromRoute]UsernameRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GetByUsernameAsync(model.Username);
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return Ok(userDTO);
        }
        /// <summary>
        /// Get user by post he created
        /// </summary>
        /// <param name="postid">Id of created post</param>
        /// <returns>User that created post</returns>
        // GET: api/users/post?postid=123..23
        [HttpGet]
        [Route("post")]
        public async Task<IActionResult> GetByPost(IdRequest model)
        {
            var post = await _postService.GetByIdAsync(model.Id);
            var user = await _userService.GetByIdAsync(post.CreatorUserId);
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return Ok(userDTO);
        }
        /// <summary>
        /// Get current authorized user
        /// </summary>
        /// <returns>Current user</returns>
        // GET: api/users/current
        [HttpGet]
        [Route("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrent()
        {
            var user = await _userService.GetCurrentUserAsync();
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return Ok(userDTO);
        }
        /// <summary>
        /// Subscribe on user
        /// </summary>
        /// <param name="model">User to subscribe model</param>
        /// <returns>Current subscription state(subscribed or not)</returns>
        // GET: api/users/subscribe/123..23
        [HttpGet]
        [Authorize]
        [Route("subscribe")]
        public async Task<IActionResult> SubscribeUser([FromRoute]UsernameRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subscription = await _userService.GetByUsernameAsync(model.Username);
            var isSubscribed = await _userService.MakeSubscription(subscription);

            return Ok(new CurrentStateResponse() { State = isSubscribed});
        }
    }
}