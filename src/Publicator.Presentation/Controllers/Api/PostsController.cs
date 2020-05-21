using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;
using Publicator.Presentation.RequestModels;

namespace Publicator.Presentation.Controllers.Api
{
    public class PostsController : BaseController
    {
        private IPostService _postService;
        private IUserService _userService;
        private ICommunityService _communityService;
        private ITagService _tagService;
        private IMapper _mapper;
        IAggregationService _aggregationService;
        public PostsController(
            IPostService postService, 
            IMapper mapper, 
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
        }
        /// <summary>
        /// Method return hot posts with paging and filtering
        /// </summary>
        /// <param name="model">Model that represents hot request</param>
        /// <returns>Hot posts by period and page</returns>
        // GET: /api/posts/hot?period=month&page=3&pagesize=20
        [HttpGet]
        [Route("hot")]
        public async Task<IActionResult> GetHot([FromQuery]HotPostsRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _postService.PageSize = model.PageSize;
            _postService.Page = model.Page;
            _postService.Period = model.Period;

            var posts = await _postService.GetHotAsync();
            var user = await _userService.TryGetCurrentAsync();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        /// <summary>
        /// Method return subscription posts with paging and filtering
        /// </summary>
        /// <param name="model">Model with post pagination and user to get posts parameters</param>
        /// <returns>Posts be subscription</returns>
        // GET: api/posts/subscription?username=john03&page=3&pagesize=20
        [HttpGet]
        [Route("subscription")]
        public async Task<IActionResult> GetBySubscription([FromQuery]PageRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var user = await _userService.GetByUsernameAsync(model.UserName);

            _postService.Page = model.Page;
            _postService.PageSize = model.PageSize;

            var posts = await _postService.GetBySubscriptionAsync();
            var curruser = await _userService.TryGetCurrentAsync();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        /// <summary>
        /// Get new posts from all posts
        /// </summary>
        /// <param name="model">Pagination request</param>
        /// <returns>New posts</returns>
        // GET: api/posts/new?page=3&pagesize=20
        [HttpGet]
        [Route("new")]
        public async Task<IActionResult> GetNew([FromQuery]PageRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _postService.Page = model.Page;
            _postService.PageSize = model.PageSize;

            var posts = await _postService.GetNewAsync();
            var curruser = await _userService.TryGetCurrentAsync();
            var postsDTO = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="id">Id of post to get</param>
        /// <returns>Post by id</returns>
        // GET: api/posts/123..1ef3
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //[FromRoute]IdRequest model
            //if (ModelState.IsValid)
            //    return BadRequest();
            var curruser = await _userService.TryGetCurrentAsync();
            var post = await _postService.GetByIdAsync(id);
            var postDTO = _aggregationService.AggregateWithBookmarkVote(post, curruser);
            return Ok(postDTO);
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

            var curruser = await _userService.TryGetCurrentAsync();
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

            var community = model.CommunityId != null ? await _communityService.GetByIdAsync((Guid)model.CommunityId) : null;

            var tags = new List<Tag>();
            foreach (var i in model.Tags)
                tags.Add(await _tagService.CreateAsync(i));
            
            var post = await _postService.CreateAsync(model.Name, model.Content, community, tags);
            var postDTO = _mapper.Map<Post, PostDTO>(post);
            return Ok(postDTO);
        }
    }
}