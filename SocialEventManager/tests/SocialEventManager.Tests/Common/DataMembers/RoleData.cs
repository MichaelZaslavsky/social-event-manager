using System;
using System.Collections.Generic;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Enums;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class RoleData
    {
        public static IEnumerable<object[]> Role
        {
            get
            {
                yield return new object[] { GetMockRole(RoleType.User.GetDescription()) };
                yield return new object[] { GetMockRole(RoleType.Admin.GetDescription()) };
            }
        }

        public static IEnumerable<object[]> Roles
        {
            get
            {
                yield return new object[]
                {
                    new List<Role>
                    {
                        GetMockRole(RoleType.User.GetDescription()),
                        GetMockRole(RoleType.Admin.GetDescription()),
                    },
                };
            }
        }

        public static IEnumerable<object[]> RolesWithSameName
        {
            get
            {
                yield return new object[]
                {
                    new List<Role>
                    {
                        GetMockRole(RoleType.User.GetDescription()),
                        GetMockRole(RoleType.User.GetDescription()),
                    },
                };
            }
        }

        public static Role GetMockRole(string name = "User") =>
            new() { Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString().ToLower(), Name = name, NormalizedName = name.ToUpper() };
    }
}
