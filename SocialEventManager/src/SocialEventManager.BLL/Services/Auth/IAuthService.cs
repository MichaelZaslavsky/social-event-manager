using LanguageExt.Common;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.BLL.Services;

public interface IAuthService
{
    Task<Result<bool>> RegisterUserAsync(UserRegistrationDto userRegistration);

    Task<Result<bool>> ConfirmEmailAsync(ConfirmEmailDto confirmEmail);

    Task<Result<string?>> LoginAsync(UserLoginDto userLogin);

    Task ForgotPasswordAsync(ForgotPasswordDto forgotPassword);

    Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto resetPassword);
}
