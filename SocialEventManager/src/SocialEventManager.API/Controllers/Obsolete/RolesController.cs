using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Identity;

namespace SocialEventManager.API.Controllers;

/// <summary>
/// Represents roles.
/// </summary>
[ApiController]
[Route(ApiPathConstants.ApiController + nameof(Obsolete))]
[ApiVersion("1.0")]
[Consumes(MediaTypeConstants.ApplicationJson)]
[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class RolesController : ControllerBase
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="RolesController"/> class.
    /// </summary>
    /// <param name="roleManager">Provides the APIs for managing roles in a persistence store.</param>
    public RolesController(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <param name="role">The role to create.</param>
    /// <returns>An empty ActionResult.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(ApplicationRole role)
    {
        IdentityResult result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            string? errorMessage = result.Errors.ToErrorMessage();

            if (errorMessage is not null)
            {
                Log.Information(errorMessage);
            }

            return BadRequest(errorMessage);
        }

        return Ok();
    }
}
