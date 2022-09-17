using System.Diagnostics.CodeAnalysis;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Repositories.Accounts;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class AccountsRepository : GenericRepository<Account>, IAccountsRepository
{
    public AccountsRepository(IDbSession session)
        : base(session)
    {
    }
}
