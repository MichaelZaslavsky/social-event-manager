using SocialEventManager.Shared.Entities;

namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal sealed class UserRolesStorage : ListStorage<UserRolesStorage, UserRole>
{
    public override void Init()
    {
        Data = RolesStorage.Instance.Data.ConvertAll(r => new UserRole
        {
            Id = 1,
            UserId = Guid.NewGuid(),
            RoleId = r.Id,
        });
    }
}
