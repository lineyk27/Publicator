using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Publicator.Core.DTO;
using Publicator.Core.Domains.Vote.Commands;
using Publicator.Core.Domains.Post.Queries;
using Publicator.Core.Domains.Vote.Queries;

namespace Publicator.Presentation.Controllers.Api
{
    public class VotesController : BaseController
    {
        private IMediator _mediator;
        public VotesController(IMediator mediator) => _mediator = mediator;
        /// <summary>
        /// Get current voote of user (up, down or unvoted)
        /// </summary>
        /// <param name="model">What post to get vote</param>
        /// <returns>Current vote</returns>
        // GET: api/votes/current?postid=123..23
        [Authorize]
        [HttpGet]
        [Route("current")]
        [ProducesResponseType(typeof(VoteDTO), 200)]
        public async Task<IActionResult> GetCurrentUserVote([FromQuery]GetCurrentVote model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vote = await _mediator.Send<VoteDTO>(model);

            return Ok(vote);
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
        public async Task<IActionResult> VoteAsync([FromBody]VoteForPost model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vote = await _mediator.Send<VoteDTO>(model);

            return Ok(vote);
        }
        /// <summary>
        /// Get's current rating of post
        /// </summary>
        /// <param name="model">Model with post id</param>
        /// <returns>Current rating</returns>
        // GET: api/votes/rating?postId=123..23
        [HttpGet]
        [Route("rating")]// TODO: return type need to be reconsidered
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CurrentRating([FromQuery]GetPostById model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _mediator.Send(model);

            return Ok(new { post.CurrentRating });
        }
    }
}