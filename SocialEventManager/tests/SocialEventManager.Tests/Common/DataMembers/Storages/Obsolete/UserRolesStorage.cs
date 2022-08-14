// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
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
*/
