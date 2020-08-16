using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MediatR;
using Publicator.ApplicationCore.Contracts;
using Publicator.Core.DTO;
using Publicator.Core.Domains.Vote.Commands;
using Publicator.Infrastructure.Models;
using Publicator.Presentation.RequestModels;

namespace Publicator.Presentation.Controllers.Api
{
    public class VotesController : BaseController
    {
        private IPostService _postService;
        private IMapper _mapper;
        private IMediator _mediator;
        public VotesController(IPostService postService, IMapper mapper, IMediator mediator)
        {
            _postService = postService;
            _mapper = mapper;
            _mediator = mediator;
        }
        /// <summary>
        /// Get current voote of user (up, down or unvoted)
        /// </summary>
        /// <param name="model">What post to get vote</param>
        /// <returns>Current vote</returns>
        // GET: api/votes/current?postid=123..23
        [Authorize]
        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrentUserVote([FromQuery]CurrentVoteRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postService.GetByIdAsync(model.PostId);
            var currentvote = await _postService.CurrentVoteAsync(post);
            var voteDTO = _mapper.Map<Vote, VoteDTO>(currentvote);
            return Ok(voteDTO);
        }
        /// <summary>
        /// Vote post
        /// </summary>
        /// <param name="model">Model with vote info</param>
        /// <returns>Current vote</returns>
        // PUT: api/votes/vote
        [Authorize]
        [HttpPut]
        [Route("vote")]
        public async Task<IActionResult> VoteAsync([FromBody]VoteRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vote = await _mediator.Send(new VoteForPost()
            {
                Up = model.Up,
                PostId = model.PostId
            });

            return Ok(vote);
        }
        /// <summary>
        /// Get's current rating of post
        /// </summary>
        /// <param name="model">Model with post id</param>
        /// <returns>Current rating</returns>
        // GET: api/votes/rating?postId=123..23
        [HttpGet]
        [Route("rating")]
        public async Task<IActionResult> CurrentRating([FromQuery]CurrentVoteRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postService.GetByIdAsync(model.PostId);
            return Ok(new { post.CurrentRating });
        }
    }
}