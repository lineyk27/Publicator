using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;

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
        public async Task<IActionResult> GetByUsername([FromRoute]string username)
        {
            var user = await _userService.GetByUsernameAsync(username);
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
        public async Task<IActionResult> GetByPost([FromQuery]Guid postid)
        {
            var post = await _postService.GetByIdAsync(postid);
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
    }
}