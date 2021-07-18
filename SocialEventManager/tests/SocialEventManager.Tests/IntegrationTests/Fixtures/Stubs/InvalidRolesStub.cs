using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Tests.IntegrationTests.Data;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs
{
    public class InvalidRolesStub : BaseStub<Role>, IRolesRepository
    {
        public Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId) => Task.FromResult<IEnumerable<Role>>(null);

        public Task<Guid> InsertRole(Role role) => Task.FromResult<Guid>(default);

        public new Task<Role> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName) =>
            Task.FromResult(RolesData.Instance.Roles[0]);
    }
}
