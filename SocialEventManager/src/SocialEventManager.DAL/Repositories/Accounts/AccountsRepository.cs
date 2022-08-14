using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Repositories.Accounts;

public class AccountsRepository : GenericRepository<Account>, IAccountsRepository
{
    public AccountsRepository(IDbSession session)
        : base(session)
    {
    }
}
