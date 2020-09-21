using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicator.Core.DTO;
using Publicator.Infrastructure.Models;
using Publicator.Presentation.RequestModels;
using MediatR;
using Publicator.Core.Domains.Post.Queries;
using Publicator.Core.Domains.Post.Commands;
using Publicator.Core.Domains.User.Queries;

namespace Publicator.Presentation.Controllers.Api
{
    public class PostsController : BaseController
    {
        private IMediator _mediator;
        public PostsController(IMediator mediator) => _mediator = mediator;
        /// <summary>
        /// Method return hot posts with paging and filtering
        /// </summary>
        /// <param name="model">Model that represents hot request</param>
        /// <returns>Hot posts by period and page</returns>
        // GET: /api/posts/hot?period=month&page=3&pagesize=20
        [HttpGet]
        [Route("hot")]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), 200)]
        [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Any,
            VaryByQueryKeys = new[] { "period", "page", "pageSize" })]
        public async Task<IActionResult> GetHot([FromQuery]HotPostsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var req = new ListHotPosts(model.Period, model.Page,model.PageSize);

            var posts = await _mediator.Send(req);

            return Ok(posts);
        }
        /// <summary>
        /// Method return subscription posts with paging and filtering
        /// </summary>
        /// <param name="model">Model with post pagination and user to get posts parameters</param>
        /// <returns>Posts be subscription</returns>
        // GET: api/posts/subscription?username=john03&page=3&pagesize=20
        [HttpGet]
        [Route("subscription")]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), 200)]
        public async Task<IActionResult> GetBySubscription([FromQuery]PageRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var posts = await _mediator.Send<IEnumerable<PostDTO>>(new ListPostsBySubscription()
            {
                Page = model.Page,
                PageSize = model.PageSize
            });

            return Ok(posts);
        }
        /// <summary>
        /// Get new posts from all posts
        /// </summary>
        /// <param name="model">Pagination request</param>
        /// <returns>New posts</returns>
        // GET: api/posts/new?page=3&pagesize=20
        [HttpGet]
        [Route("new")]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), 200)]
        [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Any,
            VaryByQueryKeys = new[] { "page", "pageSize" })]
        public async Task<IActionResult> GetNew([FromQuery]PageRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var posts = await _mediator.Send(new ListNewPosts(model.Page, model.PageSize));

            return Ok(posts);
        }
        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="id">Id of post to get</param>
        /// <returns>Post by id</returns>
        // GET: api/posts/123..1ef3
        [HttpGet]
        [Route("{id:guid}")]
        [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Any,
            VaryByQueryKeys = new[] { "id" })]
        [ProducesResponseType(typeof(PostDTO), 200)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _mediator.Send(new GetPostById() { PostId = id });
            var loggedUser = await _mediator.Send(new LoggedInUser());

            // TODO: add aggregation
            //var postDTO = _aggregationService.AggregateWithBookmarkVote(post, loggedUser);
            return Ok(post);
        }
        /// <summary>
        /// Get posts created by user
        /// </summary>
        /// <param name="model"> Model represents paginated user posts by username</param>
        /// <returns>Posts created by user</returns>
        // GET: api/posts/user?username=john03&page=3&pagesize=20
        [HttpGet]
        [Route("user")]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), 200)]
        public async Task<IActionResult> GetByCreatorUser([FromQuery]UserPostsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var posts = await _mediator.Send(new ListPostsByCreatorUser()
            {
                Page = model.Page,
                PageSize = model.PageSize,
                Username = model.Username
            });

            return Ok(posts);
        }
        /// <summary>
        /// Get posts posted in community
        /// </summary>
        /// <param name="model">Model represents paginated user posts posted in community</param>
        /// <returns></returns>
        // GET: api/posts/community?communityid=123..32&page=3&pagesize=20
        [HttpGet]
        [Route("community")]
        public async Task<IActionResult> GetByCommunity([FromQuery]CommunityPostsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var posts = await _mediator.Send(new ListPostsByCommunity()
            {
                CommunityId = model.CommunityId,
                Page = model.Page,
                PageSize = model.PageSize
            });
            
            return Ok(posts);
        }
        /// <summary>
        /// Post created post
        /// </summary>
        /// <param name="model">Model with post info</param>
        /// <returns>Ok post if all ok</returns>
        // GET: api/posts/create
        [Authorize]
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(PostDTO), 200)]
        public async Task<IActionResult> CreatePost([FromBody]CreatePostRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _mediator.Send(new CreateNewPost()
            {
                Name = model.Name,
                Content = model.Content,
                CommunityId = (Guid)model.CommunityId,
                Tags = model.Tags
            });
            
            return Ok(post);
        }
    }
}