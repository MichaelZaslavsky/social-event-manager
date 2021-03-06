namespace SocialEventManager.Tests.IntegrationTests.Infrastructure;

public class TestBase : IDisposable
{
    protected TestBase(IInMemoryDatabase db)
    {
        Db = db;
    }

    protected IInMemoryDatabase Db { get; }

    public void Dispose()
    {
        Db.CleanupAsync().GetAwaiter().GetResult();

        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
    }
}
