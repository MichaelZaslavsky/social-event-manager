using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Tests.Common.DataMembers.Storages.Identity;

using SocialEventManager.Tests.Common.DataMembers.Identity;

internal sealed class RoleStorage : ListStorage<RoleStorage, IdentityRole<Guid>>
{
    public override void Init()
    {
        Data = new()
        {
            RoleData.GetRole(RoleData.Id, UserRoles.User),
        };
    }
}
