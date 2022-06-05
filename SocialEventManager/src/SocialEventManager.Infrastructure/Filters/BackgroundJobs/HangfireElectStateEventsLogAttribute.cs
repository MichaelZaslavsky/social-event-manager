using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.States;

namespace SocialEventManager.Infrastructure.Filters.BackgroundJobs;

public class HangfireElectStateEventsLogAttribute : JobFilterAttribute, IElectStateFilter
{
    private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

    public void OnStateElection(ElectStateContext context)
    {
        if (context.CandidateState is FailedState failedState)
        {
            Logger.WarnFormat(
                "IElectStateFilter: Job `{0}` has been failed due to an exception `{1}`",
                context.BackgroundJob.Id,
                failedState.Exception);
        }
    }
}
