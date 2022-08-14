using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Tests.Common.DataMembers.Storages;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

internal class StubInvalidRoles : StubBase<Role>, IRolesRepository
{
    public Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId) => Task.FromResult<IEnumerable<Role>>(null!);

    public Task<Guid> InsertRole(Role role) => Task.FromResult<Guid>(default);

    public override Task<Role?> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName) =>
        Task.FromResult(RolesStorage.Instance.Data?[0]);
}
