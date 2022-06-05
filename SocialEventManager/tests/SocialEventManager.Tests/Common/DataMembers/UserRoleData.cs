using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Enums;
using SocialEventManager.Shared.Extensions;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

public static class UserRoleData
{
    public static TheoryData<UserRole> UserRole =>
        new() { GetMockUserRole() };

    public static TheoryData<Account, Role> UserRoleRelatedData =>
        new() { { AccountData.GetMockAccount(), RoleData.GetMockRole() } };

    public static TheoryData<Account, List<Role>> UserRoleRelatedDataWithMultipleRoles =>
        new()
        {
            {
                AccountData.GetMockAccount(),
                new()
                {
                    RoleData.GetMockRole(RoleType.User.GetDescription()),
                    RoleData.GetMockRole(RoleType.Admin.GetDescription()),
                }
            },
        };

    public static TheoryData<List<Account>, Role> UserRoleRelatedDataWithMultipleAccounts =>
        new()
        {
            {
                new()
                {
                    AccountData.GetMockAccount(),
                    AccountData.GetMockAccount(),
                },
                RoleData.GetMockRole()
            },
        };

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
