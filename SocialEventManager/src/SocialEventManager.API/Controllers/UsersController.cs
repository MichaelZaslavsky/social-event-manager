using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialEventManager.BLL.Models;
using SocialEventManager.BLL.Services;
using SocialEventManager.Infrastructure.Loggers;

namespace SocialEventManager.API.Controllers
{
    // Temp class - for test purposes
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;
        private readonly IScopeInformation _scopeInfo;

        public UsersController(IUsersService usersService, ILogger<UsersController> logger, IScopeInformation scopeInfo)
        {
            _usersService = usersService;
            _logger = logger;
            _scopeInfo = scopeInfo;
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
            // Usage of logging only for test purposes
            using (_logger.BeginScope($"Searching user by externalId: {id}."))
            using (_logger.BeginScope(_scopeInfo.HostScopeInfo))
            {
                UserDto user = await _usersService.GetUser(id);
                return Ok(user);
            }
        }
    }
}
