using System.Globalization;
using SocialEventManager.DAL.Entities;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal sealed class RolesStorage : ListStorage<RolesStorage, Role>
{
    public override void Init()
    {
        string name = RandomGeneratorHelpers.GenerateRandomValue();

        Data = new List<Role>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ConcurrencyStamp = RandomGeneratorHelpers.GenerateRandomValue(),
                Name = RandomGeneratorHelpers.GenerateRandomValue(),
                NormalizedName = name.ToUpper(CultureInfo.InvariantCulture),
            },
        };
    }
}
