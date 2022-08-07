using FluentAssertions;
using SocialEventManager.Infrastructure.Filters.BackgroundJobs;
using SocialEventManager.Tests.Common.Mocks;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.AttributeTests;

public class HangfireServerEventsLogAttributeTests
{
    [Fact]
    public void OnPerforming_Should_NotThrowException_When_Called()
    {
        HangfireServerEventsLogAttribute serverEventsLog = new();
        PerformContextMock context = new();

        Exception? executingException = Record.Exception(() => serverEventsLog.OnPerforming(new(context.Object)));
        executingException.Should().BeNull();
    }

    [Fact]
    public void OnPerformed_Should_NotThrowException_When_Called()
    {
        HangfireServerEventsLogAttribute serverEventsLog = new();
        PerformedContextMock context = new();

        Exception? executingException = Record.Exception(() => serverEventsLog.OnPerformed(context.Object));
        executingException.Should().BeNull();
    }
}
