using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.BLL.Services;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistration);

    Task<(UserLoginResult Result, string? Token)> LoginAsync(UserLoginDto userLogin);

    Task ForgotPasswordAsync(ForgotPasswordDto forgotPassword);

    Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword);
}
