using FluentAssertions;
using SocialEventManager.Infrastructure.Filters.BackgroundJobs;
using SocialEventManager.Tests.Common.Mocks;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.FilterTests;

public class HangfireClientEventsLogAttributeTests
{
    [Fact]
    public void OnCreating_Should_NotThrowException_When_Called()
    {
        HangfireClientEventsLogAttribute clientEventsLog = new();
        CreatingContextMock context = new();

        Exception? executingException = Record.Exception(() => clientEventsLog.OnCreating(context.Object));
        executingException.Should().BeNull();
    }

    [Fact]
    public void OnCreated_Should_NotThrowException_When_Called()
    {
        HangfireClientEventsLogAttribute clientEventsLog = new();
        CreatedContextMock context = new();

        Exception? executingException = Record.Exception(() => clientEventsLog.OnCreated(context.Object));
        executingException.Should().BeNull();
    }
}
