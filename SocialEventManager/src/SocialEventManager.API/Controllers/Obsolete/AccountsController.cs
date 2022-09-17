// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF.

/*
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Shared.Models.Users;

namespace SocialEventManager.API.Controllers;

using Microsoft.AspNetCore.Identity;

/// <summary>
/// Represents user accounts.
/// </summary>
[ApiController]
[Route(ApiPathConstants.ApiController)]
[ApiVersion("1.0")]
[Consumes(MediaTypeConstants.ApplicationJson)]
public sealed class AccountsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountsController"/> class.
    /// </summary>
    /// <param name="userManager">Provides the APIs for managing user in a persistence store.</param>
    /// <param name="signInManager">Provides the APIs for user sign in.</param>
    /// <param name="mapper">Provides objects mappings.</param>
    public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Registers user to the application.
    /// </summary>
    /// <param name="user">The user to register.</param>
    /// <returns>An empty ActionResult.</returns>
    [HttpPost]
    [Route(ApiPathConstants.Action)]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> Register(RegisterUserDto user)
    {
        ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(user);
        IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);

        if (!result.Succeeded)
        {
            string? errorMessage = result.Errors.ToErrorMessage();

            if (errorMessage is not null)
            {
                Log.Information(errorMessage);
            }

            return BadRequest(errorMessage);
        }

        ApplicationUser currentUser = await _userManager.FindByNameAsync(user.UserName);

        string role = RoleType.User.GetDescription();
        await _userManager.AddToRoleAsync(currentUser, role);

        await _userManager.AddClaimsAsync(applicationUser, new Claim[]
        {
            new(ClaimTypes.Sid, currentUser.Id),
            new(ClaimTypes.Email, currentUser.Email),
            new(ClaimTypes.Role, role),
        });

        return Ok();
    }

    /// <summary>
    /// Logins the specified user into the application.
    /// </summary>
    /// <param name="user">The user and password for login.</param>
    /// <returns>An empty ActionResult.</returns>
    [HttpPost]
    [Route(ApiPathConstants.Action)]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginModel user)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, true);

        if (result.IsLockedOut)
        {
            return Unauthorized();
        }

        return result.Succeeded
            ? Ok()
            : BadRequest();
    }

    /// <summary>
    /// Logouts the current user from the application.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiPathConstants.Action)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}
*/
