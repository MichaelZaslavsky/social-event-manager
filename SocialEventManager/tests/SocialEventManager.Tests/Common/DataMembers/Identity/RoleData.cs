using System.Globalization;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Tests.Common.DataMembers.Identity;

internal static class RoleData
{
    public static readonly Guid Id = Guid.NewGuid();

    public static IdentityRole<Guid> GetRole(Guid? roleId = null, string name = UserRoles.User)
    {
        return new()
        {
            Id = roleId ?? Id,
            Name = name,
            NormalizedName = name.ToUpper(CultureInfo.InvariantCulture),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        };
    }
}
