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
