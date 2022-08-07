using Hangfire.Client;

namespace SocialEventManager.Tests.Common.Mocks;

internal class CreatingContextMock
{
    private readonly Lazy<CreatingContext> _context;

    public CreatingContextMock()
    {
        _context = new(() => new(new CreateContextMock().Object), true);
    }

    public CreatingContext Object => _context.Value;
}
