using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;
using Publicator.Presentation.RequestModels;

namespace Publicator.Presentation.Controllers.Api
{
    public class PostsController : BaseController
    {
        private IPostService _postService;
        private IUserService _userService;
        private IMapper _mapper;
        public PostsController(IPostService postService, IMapper mapper, IUserService userService)
        {
            _postService = postService;
            _mapper = mapper;
            _userService = userService;
        }
        /// <summary>
        /// Method return hot posts with paging and filtering
        /// </summary>
        /// <param name="model">Model that represents hot request</param>
        /// <returns>Hot posts by period and page</returns>
        // GET: /api/posts/hot?period=month&page=3&pagesize=20
        [HttpGet]
        [Route("hot")]
        public async Task<IActionResult> GetHot([FromQuery]HotPostsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _postService.PageSize = model?.PageSize ?? _postService.PageSize;
            _postService.Page = model?.Page ?? _postService.Page;
            _postService.Period = model?.Period ?? _postService.Period;

            var posts = await _postService.GetHotAsync();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        /// <summary>
        /// Method return subscription posts with paging and filtering
        /// </summary>
        /// <param name="model">Model with post pagination and user to get posts parameters</param>
        /// <returns>Posts be subscription</returns>
        // GET: api/posts/subscription?username=john03&page=3&pagesize=20
        [HttpGet]
        [Route("subscription")]
        public async Task<IActionResult> GetBySubscription([FromQuery]SubscriptionPostsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GetByUsernameAsync(model.UserName);

            _postService.Page = model?.Page ?? _postService.Page;
            _postService.PageSize = model?.PageSize ?? _postService.PageSize;

            var posts = await _postService.GetBySubscriptionAsync(user);
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        /// <summary>
        /// Get new posts from all posts
        /// </summary>
        /// <param name="model">Pagination request</param>
        /// <returns>New posts</returns>
        // GET: api/posts/subscription?page=3&pagesize=20
        [HttpGet]
        [Route("new")]
        public async Task<IActionResult> GetNew([FromQuery]PageRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _postService.Page = model?.Page ?? _postService.Page;
            _postService.PageSize = model?.PageSize ?? _postService.PageSize;

            var posts = await _postService.GetNewAsync();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="id">Id of post to get</param>
        /// <returns>Post by id</returns>
        // GET: api/posts/123..1ef3
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _postService.GetByIdAsync(id);
            var postDTO = _mapper.Map<Post, PostDTO>(post);
            return Ok(postDTO);
        }
    }
}