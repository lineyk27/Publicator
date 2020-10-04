using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicator.Core.DTO;
using Publicator.Core.Domains.Community.Queries;
using Publicator.Infrastructure.Models;
using Publicator.Presentation.RequestModels;
using Publicator.Presentation.ResponseModels;

namespace Publicator.Presentation.Controllers.Api
{
    /// <summary>
    /// Communities controller
    /// </summary>
    public class CommunitiesController : BaseController
    {
        private IMediator _mediator;
        public CommunitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get community by id
        /// </summary>
        /// <param name="model">Id of community model</param>
        /// <returns>Community by id</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommunityDTO), 200)]
        public async Task<IActionResult> GetById([FromRoute]GetCommunityById model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var community = await _mediator.Send(model);
            
            return Ok(community);
        }
        /// <summary>
        /// Get all communities
        /// </summary>
        /// <returns>All communities</returns>
        // GET: api/communities/all
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<CommunityDTO>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var communities = await _mediator.Send(new ListAllCommunities());

            return Ok(communities);
        }
    }
}