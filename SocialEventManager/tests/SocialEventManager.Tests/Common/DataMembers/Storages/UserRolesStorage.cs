using SocialEventManager.DAL.Entities;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal sealed class UserRolesStorage : ListStorage<UserRolesStorage, UserRole>
{
    public override void Init()
    {
        Data = RolesStorage.Instance.Data.ConvertAll(r => new UserRole
        {
            Id = RandomGeneratorHelpers.NextInt32(),
            UserId = Guid.NewGuid(),
            RoleId = r.Id,
        });
    }
}
