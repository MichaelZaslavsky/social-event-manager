// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.Globalization;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal sealed class AccountsStorage : ListStorage<AccountsStorage, Account>
{
    public override void Init()
    {
        string name = RandomGeneratorHelpers.GenerateRandomValue();

        Data = new List<Account>
        {
            new()
            {
                Id = 1,
                UserId = Guid.NewGuid(),
                UserName = name,
                PasswordHash = RandomGeneratorHelpers.GenerateRandomValue() + "=",
                Email = TestConstants.ValidEmail,
                EmailConfirmed = true,
                NormalizedEmail = TestConstants.ValidEmail.ToUpper(),
                NormalizedUserName = name.ToUpper(CultureInfo.InvariantCulture),
                ConcurrencyStamp = RandomGeneratorHelpers.GenerateRandomValue(),
                SecurityStamp = RandomGeneratorHelpers.GenerateRandomValue(),
            },
        };
    }
}
*/
