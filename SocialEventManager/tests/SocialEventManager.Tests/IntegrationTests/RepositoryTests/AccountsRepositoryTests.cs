using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Accounts;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests
{
    [Collection(DataConstants.RepositoryTests)]
    [IntegrationTest]
    [Category(CategoryConstants.Identity)]
    public class AccountsRepositoryTests : RepositoryTestBase<IAccountsRepository, Account>
    {
        public AccountsRepositoryTests(IInMemoryDatabase db, IAccountsRepository accountsRepository)
            : base(db, accountsRepository)
        {
        }

        [Theory]
        [MemberData(nameof(AccountData.AccountWithSameUserId), MemberType = typeof(AccountData))]
        public async Task InsertDuplicateAccountUserId_ShouldReturnException(IEnumerable<Account> accounts)
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(accounts));
            Assert.StartsWith(ExceptionConstants.ViolationOfPrimaryKeyConstraint("PK__Accounts__"), ex.Message);
        }

        [Theory]
        [MemberData(nameof(AccountData.AccountWithValidLength), MemberType = typeof(AccountData))]
        [MemberData(nameof(AccountData.AccountWithNonRequiredNullField), MemberType = typeof(AccountData))]
        public async Task InsertAsync_ValidData(Account account)
        {
            await Db.InsertAsync(account);
        }

        [Theory]
        [MemberData(nameof(AccountData.AccountWithSameEmail), MemberType = typeof(AccountData))]
        public async Task InsertDuplicateAccountEmail_ShouldReturnException(IEnumerable<Account> accounts)
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(accounts));

            string uniqueConstraintName = $"UC_{AliasConstants.Accounts}_{nameof(Account.Email)}";
            string duplicateKeyValue = $"{accounts.First().Email}";
            Assert.Equal(ExceptionConstants.ViolationOfUniqueKeyConstraint(uniqueConstraintName, TableNameConstants.Accounts, duplicateKeyValue), ex.Message);
        }

        [Theory]
        [MemberData(nameof(AccountData.AccountWithMissingRequiredFields), MemberType = typeof(AccountData))]
        [MemberData(nameof(AccountData.AccountWithExceededLength), MemberType = typeof(AccountData))]
        public async Task InsertAsync_InvalidData(Account account, string expectedResult)
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(account));
            Assert.Equal(expectedResult, ex.Message);
        }
    }
}
