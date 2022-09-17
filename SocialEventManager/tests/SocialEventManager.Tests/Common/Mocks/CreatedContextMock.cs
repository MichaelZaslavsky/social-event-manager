using Hangfire.Client;

namespace SocialEventManager.Tests.Common.Mocks;

internal sealed class CreatedContextMock
{
    private readonly Lazy<CreatedContext> _context;

    public CreatedContextMock()
    {
        _context = new(() => new(new CreateContextMock().Object, null, false, null), true);
    }

    public CreatedContext Object => _context.Value;
}
