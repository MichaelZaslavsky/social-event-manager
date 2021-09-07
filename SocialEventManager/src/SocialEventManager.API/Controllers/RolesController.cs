using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.API.Controllers
{
    [ApiController]
    [Route(ApiPathConstants.ApiController)]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            IdentityResult result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                string errorMessage = result.Errors.ToErrorMessage();
                Log.Information(errorMessage);

                return BadRequest(errorMessage);
            }

            return Ok();
        }
    }
}
