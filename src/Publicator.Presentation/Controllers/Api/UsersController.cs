using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;

namespace Publicator.Presentation.Controllers.Api
{
    public class UsersController : BaseController
    {
        IMapper _mapper;
        IUserService _userService;
        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> GetByUsername([FromRoute]string username)
        {
            var user = await _userService.GetByUsernameAsync(username);
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return Ok(userDTO);
        }
    }
}