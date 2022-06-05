using System.Globalization;
using SocialEventManager.DAL.Entities;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.IntegrationTests.Data;

public sealed class RolesData
{
    private RolesData()
    {
        string name = RandomGeneratorHelpers.GenerateRandomValue();

        Roles = new List<Role>
            {
                new Role
                {
                    Id = Guid.NewGuid(),
                    ConcurrencyStamp = RandomGeneratorHelpers.GenerateRandomValue(),
                    Name = RandomGeneratorHelpers.GenerateRandomValue(),
                    NormalizedName = name.ToUpper(CultureInfo.InvariantCulture),
                },
            };
    }

    private static readonly Lazy<RolesData> Lazy = new(() => new RolesData());

    public static RolesData Instance
    {
        get
        {
            return Lazy.Value;
        }
    }

    public IList<Role> Roles { get; set; }
}
