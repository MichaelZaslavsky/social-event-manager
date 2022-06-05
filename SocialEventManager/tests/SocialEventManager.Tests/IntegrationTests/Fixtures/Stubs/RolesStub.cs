using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Tests.IntegrationTests.Data;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

public class RolesStub : BaseStub<Role>, IRolesRepository
{
    public Task<Guid> InsertRole(Role role)
    {
        RolesData.Instance.Roles.Add(role);
        return Task.FromResult(role.Id);
    }

    public Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId)
    {
        IEnumerable<Guid> userRoles = UserRolesData.Instance.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId);

        return Task.FromResult(RolesData.Instance.Roles.Where(r => userRoles.All(ur => ur == r.Id)));
    }

    public new Task<Role> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName)
    {
        Role role = columnName switch
        {
            nameof(Role.Id) => RolesData.Instance.Roles.SingleOrDefault(r => r.Id == (Guid)(object)filterValue),
            nameof(Role.Name) => RolesData.Instance.Roles.SingleOrDefault(r => r.Name == (string)(object)filterValue),
            nameof(Role.NormalizedName) => RolesData.Instance.Roles.SingleOrDefault(r => r.NormalizedName == (string)(object)filterValue),
            _ => null,
        };

        return Task.FromResult(role);
    }
}
