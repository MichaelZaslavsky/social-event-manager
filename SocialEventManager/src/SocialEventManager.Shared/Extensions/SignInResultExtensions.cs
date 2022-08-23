using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Enums;

namespace SocialEventManager.Shared.Extensions;

public static class SignInResultExtensions
{
    public static UserLoginResult ToUserLoginResult(this SignInResult result)
    {
        if (result == SignInResult.Failed)
        {
            return UserLoginResult.IncorrectPassword;
        }

        return result.IsLockedOut
            ? UserLoginResult.Locked
            : UserLoginResult.Success;
    }
}
