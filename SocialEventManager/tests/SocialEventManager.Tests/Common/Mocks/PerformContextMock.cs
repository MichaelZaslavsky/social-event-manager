using Hangfire;
using Hangfire.Server;
using Hangfire.Storage;
using Moq;

namespace SocialEventManager.Tests.Common.Mocks;

internal sealed class PerformContextMock
{
    private readonly Lazy<PerformContext> _context;

    public PerformContextMock()
    {
        Storage = new();
        Connection = new();
        BackgroundJob = new();
        CancellationToken = new();

        _context = new(() => new(Storage.Object, Connection.Object, BackgroundJob.Object, CancellationToken.Object));
    }

    public Mock<JobStorage> Storage { get; set; }

    public Mock<IStorageConnection> Connection { get; set; }

    public BackgroundJobMock BackgroundJob { get; set; }

    public Mock<IJobCancellationToken> CancellationToken { get; set; }

    public PerformContext Object => _context.Value;
}
