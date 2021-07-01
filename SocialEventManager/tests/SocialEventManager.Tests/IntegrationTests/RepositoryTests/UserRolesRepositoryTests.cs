using System.Threading.Tasks;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests
{
    [Collection(nameof(RolesRepositoryTests))]
    [IntegrationTest]
    [Category(CategoryConstants.Identity)]
    public class UserRolesRepositoryTests : RepositoryTestBase<IUserRolesRepository, UserRole>
    {
        public UserRolesRepositoryTests(IInMemoryDatabase db, IUserRolesRepository userRolesRepository)
            : base(db, userRolesRepository)
        {
            db.CreateTableIfNotExistsAsync<Account>().GetAwaiter().GetResult();
            db.CreateTableIfNotExistsAsync<Role>().GetAwaiter().GetResult();
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task InsertAsync(Account account, Role role)
        {
            await Db.InsertAsync(account);
            await Db.InsertAsync(role);

            int userRoleId = await Repository.InsertAsync(account.UserId, role.Name);
            UserRole actualRole = await Repository.GetSingleOrDefaultAsync(userRoleId, nameof(UserRole.Id));

            Assert.True(userRoleId > 0);
            Assert.Equal(role.Id, actualRole.RoleId);
            Assert.Equal(account.UserId, actualRole.UserId);
        }
    }
}
