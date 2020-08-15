using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Publicator.ApplicationCore.Contracts;
using Publicator.Presentation.RequestModels;
using Publicator.Infrastructure.Models;
using Publicator.ApplicationCore.DTO;
using Publicator.Core.Domains.Comment.Commands;
using MediatR;

namespace Publicator.Presentation.Controllers.Api
{
    public class CommentsController : BaseController
    {
        ICommentService _commentsService;
        IPostService _postService;
        IMapper _mapper;
        IMediator _mediator;
        public CommentsController(
            ICommentService commentService, 
            IPostService postService, 
            IMapper mapper,
            IMediator mediator
            )
        {
            _commentsService = commentService;
            _postService = postService;
            _mapper = mapper;
            _mediator = mediator;
        }
        /// <summary>
        /// Get post's comments
        /// </summary>
        /// <param name="model">Model that represents post's id</param>
        /// <returns>Comments</returns>
        // GET: api/comments/post?postid=123..23
        [HttpGet]
        [Route("post")]
        public async Task<IActionResult> GetByPost([FromQuery]CommentsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postService.GetByIdAsync(model.PostId);

            var comments = await _commentsService.GetByPostAsync(post);
            var commentsDTO = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
            return Ok(commentsDTO);
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

            var comment = await _mediator.Send<Comment>(new CreateNewComment()
            {
                Content = model.Text,
                PostId = model.PostId,
                ParentRepliedCommentId = model.ParentCommentId
            });

            var commentDTO = _mapper.Map<Comment, CommentDTO>(comment);
            return Ok(commentDTO);
        }
    }
}