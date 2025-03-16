using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicator.Core.Domains.Comment.Commands;
using Publicator.Core.Domains.Comment.Queries;
using Publicator.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Publicator.Presentation.Controllers.Api
{
    public class CommentsController : BaseController
    {
        IMediator _mediator;
        public CommentsController(IMediator mediator) => _mediator = mediator;
        /// <summary>
        /// Get post's comments
        /// </summary>
        /// <param name="model">Model that represents post's id</param>
        /// <returns>Comments</returns>
        // GET: api/comments/post?postid=123..23
        [HttpGet]
        [Route("post")]
        [ProducesResponseType(typeof(IEnumerable<CommentDTO>), 200)]
        public async Task<IActionResult> GetByPost([FromQuery] ListCommentsByPost model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _mediator.Send(model);

            return Ok(comments);
        }
        /// <summary>
        /// Create comment to post
        /// </summary>
        /// <param name="model">Model with comment info</param>
        /// <returns>Ok if all is ok</returns>
        // POST: api/comments/create
        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateComment(CreateNewComment model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _mediator.Send<CommentDTO>(model);

            return Ok(comment);
        }
    }
}