using System;
using System.Collections.Generic;
using SocialEventManager.DAL.Entities;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class RoleData
    {
        public static IEnumerable<object[]> Role
        {
            get
            {
                yield return new object[] { GetMockRole("User") };
                yield return new object[] { GetMockRole("Admin") };
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
                        GetMockRole("User"),
                        GetMockRole("Admin"),
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
                        GetMockRole("User"),
                        GetMockRole("User"),
                    },
                };
            }
        }

        #region Private Methods

        private static Role GetMockRole(string name) =>
            new() { Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString().ToLower(), Name = name, NormalizedName = name.ToUpper() };

        #endregion Private Methods
    }
}
