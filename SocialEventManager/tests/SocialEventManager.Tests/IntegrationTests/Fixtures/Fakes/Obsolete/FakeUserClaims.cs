// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Fakes;

internal class FakeUserClaims : StubBase<UserClaim>, IUserClaimsRepository
{
    public override Task InsertAsync(IEnumerable<UserClaim> userClaims)
    {
        List<UserClaim> userClaimsList = userClaims.ToList();

        UserClaim? userClaim = UserClaimsStorage.Instance.Data.LastOrDefault();
        int id = userClaim is null ? 1 : userClaim.Id;

        for (int i = 0; i < userClaimsList.Count; i++)
        {
            userClaimsList[i].Id = ++id;
            UserClaimsStorage.Instance.Data.Add(userClaimsList[i]);
        }

        return Task.CompletedTask;
    }

    public Task<IEnumerable<UserClaim>> GetUserClaims(string type, string value) =>
        Task.FromResult(UserClaimsStorage.Instance.Data.Where(uc => uc.Type == type && uc.Value == value));

    public Task<bool> DeleteUserClaims(IEnumerable<UserClaim> userClaims)
    {
        bool isDeleted = false;

        foreach (IEnumerable<UserClaim> userClaimsToDelete in
            from UserClaim userClaim in userClaims
            let userClaimsToDelete = UserClaimsStorage.Instance.Data.Where(
                uc => uc.UserId == userClaim.UserId && uc.Type == userClaim.Type && uc.Value == userClaim.Value)
            select userClaimsToDelete)
        {
            if (userClaimsToDelete.Any())
            {
                isDeleted = true;
            }

            foreach (UserClaim userClaimToDelete in userClaimsToDelete)
            {
                UserClaimsStorage.Instance.Data.Remove(userClaimToDelete);
            }
        }

        return Task.FromResult(isDeleted);
    }
}
*/
