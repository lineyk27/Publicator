using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Publicator.Core.DTO;
using System.Collections.Generic;
using MediatR;
using Publicator.Core.Domains.Post.Queries;
using Publicator.Core.Domains.Post.Commands;

namespace Publicator.Presentation.Controllers.Api
{
    public class BookmarksController : BaseController
    {
        IMediator _mediator;
        public BookmarksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get user bookmarks posts
        /// </summary>
        /// <returns>Bookmarks posts</returns>
        // GET: api/bookmarks/current
        [Authorize]
        [HttpGet]
        [Route("current")]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), 200)]
        public async Task<IActionResult> GetBookmarks()
        {
            var posts = await _mediator.Send(new ListBookmarkedPosts());

            return Ok(posts);
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
        [ProducesResponseType(typeof(BookmarkResult), 200)]
        public async Task<IActionResult> CreateBookmark([FromBody]AddPostToBookmarks model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send<BookmarkResult>(model);

            return Ok(result);

        }
    }
}