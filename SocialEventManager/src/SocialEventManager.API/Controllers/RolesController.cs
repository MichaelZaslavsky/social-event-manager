using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<AccountsController> _logger;

        public RolesController(RoleManager<ApplicationRole> roleManager, ILogger<AccountsController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            IdentityResult result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                string errorMessage = result.Errors.ToErrorMessage();
                _logger.LogInformation(errorMessage);

                return BadRequest(errorMessage);
            }

            return Ok();
        }
    }
}
