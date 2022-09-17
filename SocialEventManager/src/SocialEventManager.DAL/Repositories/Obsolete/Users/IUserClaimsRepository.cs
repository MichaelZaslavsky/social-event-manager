using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Repositories.Users;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
public interface IUserClaimsRepository : IGenericRepository<UserClaim>
{
    Task<IEnumerable<UserClaim>> GetUserClaims(string type, string value);

    Task<bool> DeleteUserClaims(IEnumerable<UserClaim> userClaims);
}
