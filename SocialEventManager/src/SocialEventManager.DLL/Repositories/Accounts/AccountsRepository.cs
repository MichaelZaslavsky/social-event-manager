using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Infrastructure;

namespace SocialEventManager.DLL.Repositories.Accounts
{
    public class AccountsRepository : GenericRepository<Account>, IAccountsRepository
    {
        public AccountsRepository(IDbSession session)
            : base(session)
        {
        }
    }
}
