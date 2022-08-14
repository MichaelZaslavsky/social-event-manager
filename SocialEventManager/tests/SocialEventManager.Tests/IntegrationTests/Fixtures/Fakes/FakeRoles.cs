using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Fakes;

internal class FakeRoles : StubBase<Role>, IRolesRepository
{
    public Task<Guid> InsertRole(Role role)
    {
        RolesStorage.Instance.Data.Add(role);
        return Task.FromResult(role.Id);
    }

    public Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId)
    {
        IEnumerable<Guid> userRoles = UserRolesStorage.Instance.Data
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId);

        return Task.FromResult(RolesStorage.Instance.Data.Where(r => userRoles.All(ur => ur == r.Id)));
    }

    public override Task<Role?> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName)
    {
        Role? role = columnName switch
        {
            nameof(Role.Id) => RolesStorage.Instance.Data.SingleOrDefault(r => r.Id == (Guid)(object)filterValue!),
            nameof(Role.Name) => RolesStorage.Instance.Data.SingleOrDefault(r => r.Name == (string)(object)filterValue!),
            nameof(Role.NormalizedName) => RolesStorage.Instance.Data.SingleOrDefault(r => r.NormalizedName == (string)(object)filterValue!),
            _ => null,
        };

        return Task.FromResult(role);
    }
}
