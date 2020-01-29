using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;
using Publicator.Presentation.RequestModels;

namespace Publicator.Presentation.Controllers.Api
{
    public class VotesController : BaseController
    {
        private IPostService _postService;
        private IMapper _mapper;
        public VotesController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
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
        // PUT: api/votes/vote?postid=123..23&up=true
        [Authorize]
        [HttpPut]
        [Route("vote")]
        public async Task<IActionResult> VoteAsync([FromQuery]VoteRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postService.GetByIdAsync(model.PostId);
            var current = await _postService.VoteAsync(post, model.Up);

            var voteDTO = _mapper.Map<Vote, VoteDTO>(current);
            return Ok(voteDTO);
        }
    }
}