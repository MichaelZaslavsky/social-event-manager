using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Repositories.Accounts;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
public interface IAccountsRepository : IGenericRepository<Account>
{
}
