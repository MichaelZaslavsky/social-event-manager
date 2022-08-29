// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.Text;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Extensions;

public static class IdentityErrorExtensions
{
    public static string? ToErrorMessage(this IEnumerable<IdentityError> errors)
    {
        if (errors.IsNullOrEmpty())
        {
            return null;
        }

        StringBuilder builder = new(GlobalConstants.Size);

        foreach (IdentityError error in errors)
        {
            builder
                .Append(error.Description)
                .Append('(')
                .Append(error.Code)
                .AppendLine(")");
        }

        return builder.ToString().TrimEnd();
    }
}
*/
