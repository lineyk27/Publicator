using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Publicator.Presentation.RequestModels;
using Publicator.Core.DTO;
using Publicator.Core.Domains.Comment.Commands;
using Publicator.Core.Domains.Comment.Queries;
using MediatR;

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
        public async Task<IActionResult> GetByPost([FromQuery]CommentsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _mediator.Send(new ListCommentsByPost() 
            { 
                Page = model.Page,
                PageSize = model.PageSize,
                PostId = model.PostId
            });

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
        public async Task<IActionResult> CreateComment(CreateCommentRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _mediator.Send<CommentDTO>(new CreateNewComment()
            {
                Content = model.Text,
                PostId = model.PostId,
                ParentRepliedCommentId = model.ParentCommentId
            });

            return Ok(comment);
        }
    }
}