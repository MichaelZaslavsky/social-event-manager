using Hangfire.States;

namespace SocialEventManager.Tests.Common.Mocks;

internal class ElectStateContextMock
{
    private readonly Lazy<ElectStateContext> _context;

    public ElectStateContextMock()
    {
        ApplyContext = new();
        _context = new(() => new(ApplyContext.Object));
    }

    public ApplyStateContextMock ApplyContext { get; set; }

    public ElectStateContext Object => _context.Value;
}
