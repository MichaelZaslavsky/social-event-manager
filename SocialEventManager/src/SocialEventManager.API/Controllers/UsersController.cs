using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialEventManager.BLL.Models;
using SocialEventManager.BLL.Services;

namespace SocialEventManager.API.Controllers
{
    // Temp class - for test purposes
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            int id = await _usersService.CreateUser(user);
            return Ok(id);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public async Task<IActionResult> GetUser(Guid id)
        {
            UserDto user = await _usersService.GetUser(id);
            return Ok(user);
        }
    }
}
