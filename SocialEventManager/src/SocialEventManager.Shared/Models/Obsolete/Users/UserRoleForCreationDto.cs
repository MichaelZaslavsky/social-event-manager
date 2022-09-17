using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Users;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public record UserRoleForCreationDto : UserRoleBase
{
    public UserRoleForCreationDto(string userId, string roleName)
        : base(userId, roleName)
    {
    }
}
