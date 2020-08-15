using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicator.ApplicationCore.Contracts;
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
        private IPostService _postService;
        private IUserService _userService;
        private ICommunityService _communityService;
        private ITagService _tagService;
        private IMapper _mapper;
        private IAggregationService _aggregationService;
        private IMediator _mediator;
        public PostsController(
            IPostService postService, 
            IMapper mapper,
            IMediator mediator,
            IUserService userService,
            ICommunityService communityService,
            ITagService tagService,
            IAggregationService aggregationService)
        {
            _aggregationService = aggregationService;
            _postService = postService;
            _mapper = mapper;
            _userService = userService;
            _communityService = communityService;
            _tagService = tagService;
            _mediator = mediator;
        }
        /// <summary>
        /// Method return hot posts with paging and filtering
        /// </summary>
        /// <param name="model">Model that represents hot request</param>
        /// <returns>Hot posts by period and page</returns>
        // GET: /api/posts/hot?period=month&page=3&pagesize=20
        [HttpGet]
        [Route("hot")]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), 200)]
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

            var posts = await _mediator.Send(new ListPostsBySubscription()
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
        public async Task<IActionResult> GetByCreatorUser([FromQuery]UserPostsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            _postService.Page = model.Page;
            _postService.PageSize = model.PageSize;

            var user = await _userService.GetByUsernameAsync(model.Username);
            var posts = await _postService.GetByCreatorAsync(user);
            var curruser = await _userService.TryGetCurrentAsync();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
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

            var community = await _communityService.GetByIdAsync(model.CommunityId);
            var posts = await _postService.GetByCommunity(community);
            var curruser = await _userService.TryGetCurrentAsync();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        /// <summary>
        /// Get posts by search search
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // GET: api/posts/search?query=any query search&startdate=2019-10-21&enddate=2019-12-31&page=3&pagesize=20
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetBySearch([FromQuery]SearchRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _postService.Page = model.Page;
            _postService.PageSize = model.PageSize;

            var community = model.CommunityId != null ? await _communityService.GetByIdAsync((Guid)model.CommunityId) : null;
            
            var posts = await _postService.GetBySearchAsync(
                model.Query,
                model.StartDate,
                model.EndDate,
                model.MinimumRating,
                community,
                null);

            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
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
        public async Task<IActionResult> CreatePost([FromBody]CreatePostRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var community = model.CommunityId == null ? null : await _communityService.GetByIdAsync((Guid)model.CommunityId);

            //var tags =  await _tagService.CreateAsync(model.Tags);

            //var post = await _postService.CreateAsync(model.Name, model.Content, community, tags);

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