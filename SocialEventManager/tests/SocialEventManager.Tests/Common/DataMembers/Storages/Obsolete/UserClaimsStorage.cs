// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.Security.Claims;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal sealed class UserClaimsStorage : ListStorage<UserClaimsStorage, UserClaim>
{
    public override void Init()
    {
        int id = 1;

        foreach (Account account in AccountsStorage.Instance.Data)
        {
            Data.Add(new()
            {
                Id = id++,
                UserId = account.UserId,
                Type = ClaimTypes.Sid,
                Value = account.UserId.ToString(),
            });

            Data.Add(new()
            {
                Id = id++,
                UserId = account.UserId,
                Type = ClaimTypes.Email,
                Value = account.Email,
            });

            foreach (Role role in RolesStorage.Instance.Data)
            {
                Data.Add(new()
                {
                    Id = id++,
                    UserId = account.UserId,
                    Type = ClaimTypes.Role,
                    Value = role.Name,
                });
            }
        }
    }
}
*/
