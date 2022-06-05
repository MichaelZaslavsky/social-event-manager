using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.DAL.Repositories.Users;

public interface IUserClaimsRepository : IGenericRepository<UserClaim>
{
    Task<IEnumerable<UserClaim>> GetUserClaims(string type, string value);

    Task<bool> DeleteUserClaims(IEnumerable<UserClaim> userClaims);
}
