using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Exceptions;

namespace SocialEventManager.Shared.Extensions;

public static class IdentityResultExtensions
{
    public static Result<bool> ToResult(this IdentityResult identityResult)
    {
        return identityResult == IdentityResult.Success
            ? new(true)
            : new(new UnprocessableEntityException(BuildErrorDescriptions(identityResult)));
    }

    private static string BuildErrorDescriptions(IdentityResult result) => string.Join(", ", result.Errors.Select(error => error.Description));
}
