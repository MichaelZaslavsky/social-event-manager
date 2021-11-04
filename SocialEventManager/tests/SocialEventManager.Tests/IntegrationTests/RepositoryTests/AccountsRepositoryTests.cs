using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Accounts;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;
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
        public async Task InsertDuplicateAccountUserId_Should_Throw_SqlException(IEnumerable<Account> accounts)
        {
            Func<Task> func = async () => await Db.InsertAsync(accounts);
            string message = (await func.Should().ThrowAsync<SqlException>()).Subject.First().Message;
            message.Should().StartWith(ExceptionConstants.ViolationOfPrimaryKeyConstraint("PK__Accounts__"));
        }

        [Theory]
        [MemberData(nameof(AccountData.AccountWithValidLength), MemberType = typeof(AccountData))]
        [MemberData(nameof(AccountData.AccountWithNonRequiredNullField), MemberType = typeof(AccountData))]
        public async Task InsertAsync_ValidData_Should_Succeed(Account account)
        {
            await Db.InsertAsync(account);
        }

        [Theory]
        [MemberData(nameof(AccountData.AccountWithSameEmail), MemberType = typeof(AccountData))]
        public async Task InsertDuplicateAccountEmail_Should_Throw_SqlException(IEnumerable<Account> accounts)
        {
            string uniqueConstraintName = $"UC_{AliasConstants.Accounts}_{nameof(Account.Email)}";
            string duplicateKeyValue = $"{accounts.First().Email}";
            string expectedMessage = ExceptionConstants.ViolationOfUniqueKeyConstraint(uniqueConstraintName, TableNameConstants.Accounts, duplicateKeyValue);

            Func<Task> func = async () => await Db.InsertAsync(accounts);
            await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(AccountData.AccountWithMissingRequiredFields), MemberType = typeof(AccountData))]
        public async Task InsertAsync_MissingRequiredFields_Should_Throw_SqlException(Account account, string expectedMessage)
        {
            Func<Task> func = async () => await Db.InsertAsync(account);
            await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(AccountData.AccountWithExceededLength), MemberType = typeof(AccountData))]
        public async Task InsertAsync_ExceedLength_Should_Throw_SqlException(Account account, string expectedMessage)
        {
            Func<Task> func = async () => await Db.InsertAsync(account);
            (await func.Should().ThrowAsync<SqlException>()).And.Message.Should().StartWith(expectedMessage);
        }
    }
}
