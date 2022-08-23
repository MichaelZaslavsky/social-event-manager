using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Enums;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers.Identity;

internal static class SignInResultData
{
    public static TheoryData<SignInResult, UserLoginResult> SignInResultToUserLoginResult =>
        new()
        {
            {
                SignInResult.Success,
                UserLoginResult.Success
            },
            {
                SignInResult.Failed,
                UserLoginResult.IncorrectPassword
            },
            {
                SignInResult.LockedOut,
                UserLoginResult.Locked
            },
        };
}
