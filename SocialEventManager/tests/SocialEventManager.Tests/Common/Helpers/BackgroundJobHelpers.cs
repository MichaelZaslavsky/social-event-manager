using SocialEventManager.Shared.Enums;
using SocialEventManager.Tests.Common.DataMembers.Storages;

namespace SocialEventManager.Tests.Common.Helpers;

internal static class BackgroundJobHelpers
{
    private const int Second = 1000;

    public static async Task WaitForCompletion(BackgroundJobType jobType, int timeoutInSeconds = 30)
    {
        int maxIterations = timeoutInSeconds;

        for (int i = 0; i < maxIterations; i++)
        {
            if (BackgroundJobStorage.Instance.Data[jobType])
            {
                break;
            }

            await Task.Delay(Second);
        }
    }
}
