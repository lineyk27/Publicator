using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;
using Publicator.Presentation.RequestModels;
using Publicator.Presentation.ResponseModels;
using System.Collections.Generic;

namespace Publicator.Presentation.Controllers.Api
{
    public class BookmarksController : BaseController
    {
        IPostService _postService;
        IMapper _mapper;
        IAggregationService _aggregationService;
        IUserService _userService;
        public BookmarksController(IPostService postService, 
            IMapper mapper, 
            IAggregationService aggregationService,
            IUserService userService)
        {
            _postService = postService;
            _mapper = mapper;
            _aggregationService = aggregationService;
            _userService = userService;
        }
        /// <summary>
        /// Get user bookmarks posts
        /// </summary>
        /// <returns>Bookmarks posts</returns>
        // GET: api/bookmarks/current
        [Authorize]
        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetBookmarks()
        {
            var posts = await _postService.GetBookmarks();
            var curruser = await _userService.TryGetCurrentAsync();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        /// <summary>
        /// Create bookmark (or delete if it already exists)
        /// </summary>
        /// <param name="model">Post tha must be bookmarked</param>
        /// <returns>Curent state of bookmark(exist or not)</returns>
        // PUT: api/bookmarks/create
        [Authorize]
        [HttpPut]
        [Route("create")]
        public async Task<IActionResult> CreateBookmark([FromBody]BookmarkRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postService.GetByIdAsync(model.PostId);
            var current = await _postService.AddToBookmarkAsync(post);

            var currentState = new CurrentStateResponse() { State = current };
            
            return Ok(currentState);

        }
    }
}