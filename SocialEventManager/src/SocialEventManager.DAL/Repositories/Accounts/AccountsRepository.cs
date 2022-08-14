using SocialEventManager.Shared.Entities;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.DAL.Repositories.Accounts;

public class AccountsRepository : GenericRepository<Account>, IAccountsRepository
{
    public AccountsRepository(IDbSession session)
        : base(session)
    {
    }
}
