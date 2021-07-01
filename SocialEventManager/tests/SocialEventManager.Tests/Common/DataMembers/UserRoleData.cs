using System;
using System.Collections.Generic;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class UserRoleData
    {
        public static IEnumerable<object[]> UserRoleRelatedData
        {
            get
            {
                yield return new object[] { AccountData.GetMockAccount(), RoleData.GetMockRole() };
            }
        }

        public static IEnumerable<object[]> UserRoleRelatedDataWithMultipleRoles
        {
            get
            {
                yield return new object[]
                {
                    AccountData.GetMockAccount(),
                    new List<Role>
                    {
                        RoleData.GetMockRole(RoleType.User.GetDescription()),
                        RoleData.GetMockRole(RoleType.Admin.GetDescription()),
                    },
                };
            }
        }

        public static IEnumerable<object[]> UserRoleRelatedDataWithMultipleAccounts
        {
            get
            {
                yield return new object[]
                {
                    new List<Account>
                    {
                        AccountData.GetMockAccount(),
                        AccountData.GetMockAccount(),
                    },
                    RoleData.GetMockRole(),
                };
            }
        }

        public static UserRole GetMockUserRole(Guid roleId, Guid userId)
        {
            return new UserRole
            {
                Id = RandomGeneratorHelpers.NextInt32(),
                RoleId = roleId,
                UserId = userId,
            };
        }
    }
}
