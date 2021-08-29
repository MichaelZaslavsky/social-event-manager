using System;
using System.Collections.Generic;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class ApplicationRoleData
    {
        public static IEnumerable<object[]> InvalidApplicationRole
        {
            get
            {
                yield return new object[]
                {
                    new ApplicationRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = null,
                        NormalizedName = RandomGeneratorHelpers.GenerateRandomValue(),
                        ConcurrencyStamp = RandomGeneratorHelpers.GenerateRandomValue(),
                    },
                    ExceptionConstants.RoleNameIsInvalid,
                };
            }
        }
    }
}
