using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Publicator.ApplicationCore.Contracts;
using Publicator.Presentation.RequestModels;
using Publicator.Infrastructure.Entities;
using Publicator.ApplicationCore.DTO;

namespace Publicator.Presentation.Controllers.Api
{
    public class CommentsController : BaseController
    {
        ICommentService _commentsService;
        IPostService _postService;
        IMapper _mapper;
        public CommentsController(ICommentService commentService, IPostService postService, IMapper mapper)
        {
            _commentsService = commentService;
            _postService = postService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get post's comments
        /// </summary>
        /// <param name="model">Model that represents query with pagnation</param>
        /// <returns>Comments</returns>
        // GET: api/comments/post?postid=123..23&page=3&pagesize=10
        [HttpGet]
        [Route("post")]
        public async Task<IActionResult> GetByPost(CommentsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postService.GetByIdAsync(model.PostId);
            _commentsService.Page = model.Page;
            _commentsService.PageSize = model.PageSize;

            var comments = await _commentsService.GetByPostAsync(post);
            var commentsDTO = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
            return Ok(commentsDTO);
        }
    }
}