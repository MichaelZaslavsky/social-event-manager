using Hangfire;
using Hangfire.States;
using Hangfire.Storage;
using Moq;

namespace SocialEventManager.Tests.Common.Mocks;

internal sealed class ApplyStateContextMock
{
    private readonly Lazy<ApplyStateContext> _context;

    public ApplyStateContextMock()
    {
        Storage = new();
        Connection = new();
        Transaction = new();
        BackgroundJob = new();
        NewState = new();
        NewStateObject = NewState.Object;
        OldStateName = string.Empty;
        JobExpirationTimeout = TimeSpan.FromMinutes(1);

        _context = new(
            () => new(
                Storage.Object,
                Connection.Object,
                Transaction.Object,
                BackgroundJob.Object,
                NewStateObject ?? NewState.Object,
                OldStateName)
            {
                JobExpirationTimeout = JobExpirationTimeout,
            });
    }

    public Mock<JobStorage> Storage { get; set; }

    public Mock<IStorageConnection> Connection { get; set; }

    public Mock<IWriteOnlyTransaction> Transaction { get; set; }

    public BackgroundJobMock BackgroundJob { get; set; }

    public IState NewStateObject { get; set; }

    public Mock<IState> NewState { get; set; }

    public string OldStateName { get; set; }

    public TimeSpan JobExpirationTimeout { get; set; }

    public ApplyStateContext Object => _context.Value;
}
