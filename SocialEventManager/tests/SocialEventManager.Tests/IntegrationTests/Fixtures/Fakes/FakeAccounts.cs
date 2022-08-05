using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Accounts;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Fakes;

internal class FakeAccounts : StubBase<Account>, IAccountsRepository
{
    public override Task<Account?> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName)
    {
        Account? account = columnName switch
        {
            nameof(Account.UserId) => AccountsStorage.Instance.Data.SingleOrDefault(a => a.UserId == (Guid)(object)filterValue!),
            nameof(Account.NormalizedUserName) => AccountsStorage.Instance.Data.SingleOrDefault(a => a.NormalizedUserName == (string)(object)filterValue!),
            nameof(Account.NormalizedEmail) => AccountsStorage.Instance.Data.SingleOrDefault(a => a.NormalizedEmail == (string)(object)filterValue!),
            _ => null,
        };

        return Task.FromResult(account);
    }

    public override Task<int> InsertAsync(Account account)
    {
        Account? lastAccount = AccountsStorage.Instance.Data.LastOrDefault();
        int id = lastAccount is null ? 1 : lastAccount.Id + 1;

        account.Id = id;
        AccountsStorage.Instance.Data.Add(account);

        return Task.FromResult(id);
    }
}
