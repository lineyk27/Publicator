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
        public BookmarksController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get user bookmarks posts
        /// </summary>
        /// <returns>Bookmarks posts</returns>
        // GET: api/bookmarks
        [Authorize]
        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetBookmarks()
        {
            var posts = await _postService.GetBookmarks();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        /// <summary>
        /// Create bookmark (or delete if it already exists)
        /// </summary>
        /// <param name="model">Post tha must be bookmarked</param>
        /// <returns>Curent state of bookmark(exist or not)</returns>
        // PUT: api/bookmarks/create?postid=123..23
        [Authorize]
        [HttpPut]
        [Route("create")]
        public async Task<IActionResult> CreateBookmark([FromQuery]BookmarkRequest model)
        {
            if (ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postService.GetByIdAsync(model.PostId);
            var current = await _postService.AddToBookmarkAsync(post);

            var currentState = new CurrentStateResponse() { State = current };
            
            return Ok(currentState);

        }
    }
}