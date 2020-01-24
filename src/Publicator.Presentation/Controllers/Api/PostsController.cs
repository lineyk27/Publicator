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
        private IMapper _mapper;
        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        /// <summary>
        /// Method return hot posts by paging and filtering paging
        /// </summary>
        /// <param name="model">Model for represent paging and filtering</param>
        /// <returns>Hot posts by period and page</returns>
        // GET: /api/posts/hot
        [HttpGet]
        [Route("hot")]
        public async Task<IActionResult> GetHot([FromBody]HotRequest model)
        {
            _postService.PageSize = model?.PageSize ?? _postService.PageSize;
            _postService.Page = model?.Page ?? _postService.Page;
            _postService.Period = model?.Period ?? _postService.Period;

            var posts = await _postService.GetHotAsync();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBySubscription()
        {
            return Ok();
        }
    }
}