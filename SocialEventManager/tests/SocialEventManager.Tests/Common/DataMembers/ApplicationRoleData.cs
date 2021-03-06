using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Helpers;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

public static class ApplicationRoleData
{
    public static TheoryData<ApplicationRole, string> InvalidApplicationRole =>
        new()
        {
            {
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = null,
                    NormalizedName = RandomGeneratorHelpers.GenerateRandomValue(),
                    ConcurrencyStamp = RandomGeneratorHelpers.GenerateRandomValue(),
                },
                ExceptionConstants.RoleNameIsInvalid
            },
        };
}
