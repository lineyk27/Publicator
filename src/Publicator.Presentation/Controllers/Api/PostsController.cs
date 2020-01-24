using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicator.ApplicationCore;
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
        // GET: /api/posts/hot
        [HttpGet]
        [Route("hot")]
        public async Task<IActionResult> GetHot([FromBody]HotPostsRequest model)
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
        /// <param name="model"></param>
        /// <returns></returns>
        // GET: api/posts/subscription
        [HttpGet]
        [Route("subscription")]
        public async Task<IActionResult> GetBySubscription([FromBody]SubscriptionPostsRequest model)
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
    }
}