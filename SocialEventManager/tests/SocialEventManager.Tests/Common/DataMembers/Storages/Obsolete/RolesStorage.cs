// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.Globalization;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal sealed class RolesStorage : ListStorage<RolesStorage, Role>
{
    public override void Init()
    {
        string roleName = RoleType.User.GetDescription();

        Data = new List<Role>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ConcurrencyStamp = RandomGeneratorHelpers.GenerateRandomValue(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(CultureInfo.InvariantCulture),
            },
        };
    }
}
*/
