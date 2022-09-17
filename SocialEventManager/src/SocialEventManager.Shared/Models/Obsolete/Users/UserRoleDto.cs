using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Users;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed record UserRoleDto : UserRoleBase
{
    public UserRoleDto(string userId, string roleName)
        : base(userId, roleName)
    {
    }

    public UserRoleDto(Guid userId, string roleName)
        : base(userId, roleName)
    {
    }
}
