using System.Diagnostics.CodeAnalysis;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Fakes;

internal class FakeUserRoles : StubBase<UserRole>, IUserRolesRepository
{
    public Task<int> InsertAsync(Guid userId, string roleName)
    {
        Role? role = RolesStorage.Instance.Data.SingleOrDefault(r => r.NormalizedName == roleName);

        if (role is null)
        {
            return Task.FromResult(0);
        }

        UserRole? userRole = UserRolesStorage.Instance.Data.LastOrDefault();
        int id = userRole is null ? 1 : userRole.Id + 1;

        UserRolesStorage.Instance.Data.Add(new()
        {
            Id = id,
            UserId = userId,
            RoleId = role.Id,
        });

        return Task.FromResult(id);
    }

    public Task<IEnumerable<UserRole>> GetUserRoles(string roleName)
    {
        IEnumerable<Role> roles = RolesStorage.Instance.Data.Where(r => r.NormalizedName == roleName);

        if (roles.IsEmpty())
        {
            return Task.FromResult(Enumerable.Empty<UserRole>());
        }

        IEnumerable<UserRole> userRoles = UserRolesStorage.Instance.Data.SelectMany(
            ur => RolesStorage.Instance.Data.Where(r => ur.RoleId == r.Id).Select(_ => ur));

        return Task.FromResult(userRoles);
    }

    public Task<bool> DeleteUserRole(Guid userId, string roleName)
    {
        List<UserRole> userRoles = UserRolesStorage.Instance.Data.Where(ur => ur.UserId == userId).ToList();

        if (userRoles.IsEmpty())
        {
            return Task.FromResult(false);
        }

        bool isDeleted = false;

        foreach (UserRole userRole in UserRolesStorage.Instance.Data.SelectMany(
            ur => RolesStorage.Instance.Data.Where(r => ur.RoleId == r.Id).Select(_ => ur)))
        {
            userRoles.Remove(userRole);
            isDeleted = true;
        }

        return Task.FromResult(isDeleted);
    }

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1312:Variable names should begin with lower-case letter", Justification = "<Pending>")]
    public Task<bool> IsInRole(Guid userId, string roleName)
    {
        foreach (object _ in UserRolesStorage.Instance.Data.Where(ur => ur.UserId == userId).SelectMany(
            userRole => RolesStorage.Instance.Data.Where(role => role.NormalizedName == roleName && userRole.RoleId == role.Id).Select(_ => new { })))
        {
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}
