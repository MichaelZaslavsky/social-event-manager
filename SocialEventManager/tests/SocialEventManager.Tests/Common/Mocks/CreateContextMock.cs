using System.Diagnostics.CodeAnalysis;
using Hangfire;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using Moq;

namespace SocialEventManager.Tests.Common.Mocks;

internal sealed class CreateContextMock
{
    private readonly Lazy<CreateContext> _context;

    public CreateContextMock()
    {
        Storage = new();
        Connection = new();
        Job = Job.FromExpression(() => SomeMethod());
        InitialState = new();

        _context = new(() => new(Storage.Object, Connection.Object, Job, InitialState.Object));
    }

    public Mock<JobStorage> Storage { get; set; }

    public Mock<IStorageConnection> Connection { get; set; }

    public Job Job { get; set; }

    public Mock<IState> InitialState { get; set; }

    public CreateContext Object => _context.Value;

    [ExcludeFromCodeCoverage]
    public static void SomeMethod()
    {
        // This is an illustration of a background job.
    }
}
