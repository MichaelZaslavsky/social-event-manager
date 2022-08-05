using System.Globalization;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Enums;
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
