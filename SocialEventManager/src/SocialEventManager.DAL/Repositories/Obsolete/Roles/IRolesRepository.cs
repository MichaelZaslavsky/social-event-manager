// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Repositories.Roles;

public interface IRolesRepository : IGenericRepository<Role>
{
    Task<Guid> InsertRole(Role role);

    /*
    Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId);
    */
}
