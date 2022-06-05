using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Enums;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.Tests.Common.DataMembers;

public static class UserRoleData
{
    public static IEnumerable<object[]> UserRole
    {
        get
        {
            yield return new object[] { GetMockUserRole() };
        }
    }

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

    public static UserRole GetMockUserRole(Guid? roleId = null, Guid? userId = null)
    {
        return new UserRole
        {
            Id = 1,
            RoleId = roleId ?? Guid.NewGuid(),
            UserId = userId ?? Guid.NewGuid(),
        };
    }
}
