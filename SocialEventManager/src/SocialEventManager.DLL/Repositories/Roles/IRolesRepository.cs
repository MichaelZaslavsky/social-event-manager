using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.DAL.Repositories.Roles;

public interface IRolesRepository : IGenericRepository<Role>
{
    Task<Guid> InsertRole(Role role);

    Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId);
}
