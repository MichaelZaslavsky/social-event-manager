using Microsoft.AspNetCore.Identity;

namespace SocialEventManager.Tests.Common.DataMembers.Storages.Identity;

using SocialEventManager.Tests.Common.DataMembers.Identity;

internal sealed class UserRoleStorage : ListStorage<UserRoleStorage, IdentityUserRole<Guid>>
{
    public override void Init()
    {
        Data = new()
        {
            new()
            {
                UserId = UserData.Id,
                RoleId = RoleData.Id,
            },
        };
    }
}
