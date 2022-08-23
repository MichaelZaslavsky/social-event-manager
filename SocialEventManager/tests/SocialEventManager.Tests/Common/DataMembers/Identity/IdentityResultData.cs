using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Enums;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers.Identity;

internal static class IdentityResultData
{
    public static TheoryData<IdentityResult, UserLoginResult> IdentityResultToUserLoginResult =>
        new()
        {
            {
                GetFailedIdentityResult(nameof(AuthConstants.EmailOrPasswordIsIncorrect), AuthConstants.EmailOrPasswordIsIncorrect),
                UserLoginResult.EmailNotFound
            },
            {
                GetFailedIdentityResult(nameof(AuthConstants.UserIsLocked), AuthConstants.UserIsLocked),
                UserLoginResult.Locked
            },
            {
                IdentityResult.Success,
                UserLoginResult.Success
            },
        };

    private static IdentityResult GetFailedIdentityResult(string code, string description)
    {
        return IdentityResult.Failed(new IdentityError
        {
            Code = code,
            Description = description,
        });
    }
}
