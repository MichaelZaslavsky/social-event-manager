using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.Server;

namespace SocialEventManager.Infrastructure.Filters.BackgroundJobs;

public class HangfireServerEventsLogAttribute : JobFilterAttribute, IServerFilter
{
    private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

    public void OnPerforming(PerformingContext filterContext)
    {
        Logger.InfoFormat("IServerFilter: Starting to perform job `{0}`", filterContext.BackgroundJob.Id);
    }

    public void OnPerformed(PerformedContext filterContext)
    {
        Logger.InfoFormat("IServerFilter: Job `{0}` has been performed", filterContext.BackgroundJob.Id);
    }
}
