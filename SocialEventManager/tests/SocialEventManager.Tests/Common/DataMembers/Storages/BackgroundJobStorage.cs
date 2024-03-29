using SocialEventManager.Shared.Enums;

namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal sealed class BackgroundJobStorage : DictionaryStorage<BackgroundJobStorage, BackgroundJobType, bool>
{
    public override void Init()
    {
        Data = new Dictionary<BackgroundJobType, bool>
        {
            { BackgroundJobType.Email, false },
        };
    }
}
