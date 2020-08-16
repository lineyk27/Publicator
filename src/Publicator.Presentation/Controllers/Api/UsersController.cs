using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Models;
using Publicator.Presentation.RequestModels;
using Publicator.Presentation.ResponseModels;
using MediatR;
using Publicator.Core.Domains.User.Queries;
using Publicator.Core.Domains.Post.Queries;
using Publicator.Core.Domains.User.Commands;

namespace Publicator.Presentation.Controllers.Api
{
    public class UsersController : BaseController
    {
        IMediator _mediator;
        public UsersController(IMediator mediator) => _mediator = mediator;
        /// <summary>
        /// Get user by post he created
        /// </summary>
        /// <param name="postid">Id of created post</param>
        /// <returns>User that created post</returns>
        // GET: api/users/post?postid=123..23
        [HttpGet]
        [Route("post")]
        public async Task<IActionResult> GetByPost([FromRoute]IdRequest model)
        {
            var post = await _mediator.Send(new GetPostById() { PostId = model.Id });

            var user = await _mediator.Send(new GetUserById() { UserId = post.Id });

            return Ok(user);
        }
        /// <summary>
        /// Get's current subscription on user
        /// </summary>
        /// <param name="model">Username of queried user</param>
        /// <returns>Current state of subscription.</returns>
        // GET: api/users/currentSubscription?username=lineyk27
        [HttpGet]
        [Route("currentSubscription")]
        [ProducesResponseType(typeof(SubscriptionResult), 200)]
        public async Task<IActionResult> GetCurrentSubscription([FromQuery]UsernameRequest model){            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subscription = await _mediator.Send<SubscriptionResult>(new GetCurrentSubscription()
            {
                SubscriberUsername = model.Username
            });

            return Ok(subscription);
        }
        /// <summary>
        /// Subscribe on user
        /// </summary>
        /// <param name="model">User to subscribe model</param>
        /// <returns>Current subscription state(subscribed or not)</returns>
        // PUT: api/users/subscribe
        [HttpPut]
        [Authorize]
        [Route("subscribe")]
        [ProducesResponseType(typeof(SubscriptionResult), 200)]
        public async Task<IActionResult> SubscribeUser([FromBody]UsernameRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subscription = await _mediator.Send<SubscriptionResult>(new SubscribeToUser()
            {
                SubscriberUsername = model.Username
            });

            return Ok(subscription);
        }
        /// <summary>
        /// Gets user by username
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <returns>User found by username</returns>
        // GET: api/users?username=john03
        [HttpGet]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<IActionResult> GetByUsername([FromQuery]UsernameRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _mediator.Send(new GetByUsername()
            {
                Username = model.Username
            });

            return Ok(user);
        }
    }
}