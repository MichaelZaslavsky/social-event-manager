using Hangfire;
using Hangfire.Common;

namespace SocialEventManager.Tests.Common.Mocks;

internal class BackgroundJobMock
{
    private readonly Lazy<BackgroundJob> _object;

    public BackgroundJobMock()
    {
        Id = "JobId";
        Job = Job.FromExpression(() => SomeMethod());
        CreatedAt = DateTime.UtcNow;

        _object = new(() => new(Id, Job, CreatedAt));
    }

    public string Id { get; set; }

    public Job Job { get; set; }

    public DateTime CreatedAt { get; set; }

    public BackgroundJob Object => _object.Value;

    public static void SomeMethod()
    {
        // This is an illustration of a background job.
    }
}
