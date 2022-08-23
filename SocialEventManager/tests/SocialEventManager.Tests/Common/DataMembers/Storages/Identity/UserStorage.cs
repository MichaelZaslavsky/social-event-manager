using SocialEventManager.Shared.Models.Auth;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Tests.Common.DataMembers.Storages.Identity;

using SocialEventManager.Tests.Common.DataMembers.Identity;

internal sealed class UserStorage : ListStorage<UserStorage, ApplicationUser>
{
    public override void Init()
    {
        Data = new()
        {
            UserData.GetUser(UserData.Id, email: TestConstants.ValidEmail),
        };
    }
}
