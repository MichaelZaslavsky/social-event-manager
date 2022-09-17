using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Logging;

namespace SocialEventManager.Infrastructure.Filters.BackgroundJobs;

public sealed class HangfireClientEventsLogAttribute : JobFilterAttribute, IClientFilter
{
    private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

    public void OnCreating(CreatingContext filterContext)
    {
        Logger.InfoFormat("IClientFilter: Creating a job based on method `{0}`...", filterContext.Job.Method.Name);
    }

    public void OnCreated(CreatedContext filterContext)
    {
        Logger.InfoFormat(
            "IClientFilter: Job that is based on method `{0}` has been created with id `{1}`",
            filterContext.Job.Method.Name,
            filterContext.BackgroundJob?.Id);
    }
}
