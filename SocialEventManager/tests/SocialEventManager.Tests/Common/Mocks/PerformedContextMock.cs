using Hangfire.Server;

namespace SocialEventManager.Tests.Common.Mocks;

internal class PerformedContextMock
{
    private readonly Lazy<PerformedContext> _context;

    public PerformedContextMock()
    {
        _context = new(() => new(new PerformContextMock().Object, null, false, null), true);
    }

    public PerformedContext Object => _context.Value;
}
