using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;
using Publicator.Presentation.RequestModels;
using Publicator.Presentation.ResponseModels;

namespace Publicator.Presentation.Controllers.Api
{
    /// <summary>
    /// Communities controller
    /// </summary>
    public class CommunitiesController : BaseController
    {
        private ICommunityService _communityService;
        private IMapper _mapper;
        public CommunitiesController(ICommunityService communityService, IMapper mapper)
        {
            _communityService = communityService;
            _mapper = mapper;
        }
        /// <summary>
        /// Change community picture
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ok if all ok</returns>
        // PUT: api/communities/picture?communityid=123..23&url=https://www.../
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPut]
        [Route("picture")]
        public async Task<IActionResult> ChangeCommunityPicture([FromBody]ChangeCommunityPictureRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var community = await _communityService.GetByIdAsync(model.CommunityId);
            _communityService.ChangePicture(community, model.Url);
            return Ok();
        }
        /// <summary>
        /// Create new community
        /// </summary>
        /// <param name="model">Community info for creation</param>
        /// <returns></returns>
        // POST: api/communities/create
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCommunity([FromBody]CreateCommunityRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _communityService.CreateNewAsync(model.Name, model.Description, model.ImageUrl);
            var response = new NewResourceResponse() { Id = id };
            return Ok(response);
        }
        /// <summary>
        /// Get community by id
        /// </summary>
        /// <param name="model">Id of community model</param>
        /// <returns>Community by id</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]IdRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var community = await _communityService.GetByIdAsync(model.Id);
            var communityDTO = _mapper.Map<Community, CommunityDTO>(community);
            return Ok(communityDTO);
        }
    }
}