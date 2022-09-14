using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEventManager.BLL.Services;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.API.Controllers;

[ApiController]
[Route(ApiPathConstants.ApiController)]
[ApiVersion("1.0")]
[Consumes(MediaTypeConstants.ApplicationJson)]
[AllowAnonymous]
public class AccountsController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountsController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route(ApiPathConstants.Action)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
    public async Task<IActionResult> Register(UserRegistrationDto userRegistration)
    {
        Result<bool> result = await _authService.RegisterUserAsync(userRegistration);
        return result.ToOk(value => value);
    }

    [HttpPost]
    [Route(ApiPathConstants.Action)]
    [Consumes(MediaTypeConstants.ApplicationFormUrlEncoded)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
    public async Task<IActionResult> ConfirmEmail([FromForm] ConfirmEmailDto confirmEmailDto)
    {
        Result<bool> result = await _authService.ConfirmEmailAsync(confirmEmailDto);
        return result.ToOk(value => value);
    }

    [HttpPost]
    [Route(ApiPathConstants.Action)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    public async Task<IActionResult> Login(UserLoginDto userLogin)
    {
        Result<string?> result = await _authService.LoginAsync(userLogin);
        return result.ToOk(value => value);
    }

    [HttpPost]
    [Route(ApiPathConstants.Action)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        await _authService.ForgotPasswordAsync(forgotPasswordDto);
        return Ok();
    }

    [HttpPost]
    [Route(ApiPathConstants.Action)]
    [Consumes(MediaTypeConstants.ApplicationFormUrlEncoded)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto)
    {
        Result<bool> result = await _authService.ResetPasswordAsync(resetPasswordDto);
        return result.ToOk(value => value);
    }
}
