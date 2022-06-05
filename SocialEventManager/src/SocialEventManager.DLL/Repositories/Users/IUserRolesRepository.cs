using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.DAL.Repositories.Users;

public interface IUserRolesRepository : IGenericRepository<UserRole>
{
    Task<int> InsertAsync(Guid userId, string roleName);

    Task<IEnumerable<UserRole>> GetUserRoles(string roleName);

    Task<bool> DeleteUserRole(Guid userId, string roleName);

    Task<bool> IsInRole(Guid userId, string roleName);
}
