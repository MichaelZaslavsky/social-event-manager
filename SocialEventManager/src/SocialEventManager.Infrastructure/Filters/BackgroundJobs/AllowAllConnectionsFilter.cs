using Hangfire.Dashboard;

namespace SocialEventManager.Infrastructure.Filters.BackgroundJobs;

// A temporary solution for allowing the HangFire dashboard to work with Docker.
// At a later stage this class should be replaced with a one that will authorize the user who wants to use the Hangfire dashboard.
public class AllowAllConnectionsFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}
