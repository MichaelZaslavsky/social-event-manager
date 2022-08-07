using FluentAssertions;
using SocialEventManager.Infrastructure.Filters.BackgroundJobs;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Mocks;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.AttributeTests;

[UnitTest]
[Category(CategoryConstants.Attributes)]
public class HangfireApplyStateEventsLogAttributeTests
{
    [Fact]
    public void OnStateApplied_Should_NotThrowException_When_Called()
    {
        HangfireApplyStateEventsLogAttribute applyStateEventsLog = new();
        ApplyStateContextMock context = new();

        Exception? executingException = Record.Exception(() => applyStateEventsLog.OnStateApplied(context.Object, null!));
        executingException.Should().BeNull();
    }

    [Fact]
    public void OnStateUnapplied_Should_NotThrowException_When_Called()
    {
        HangfireApplyStateEventsLogAttribute applyStateEventsLog = new();
        ApplyStateContextMock context = new();

        Exception? executingException = Record.Exception(() => applyStateEventsLog.OnStateUnapplied(context.Object, null!));
        executingException.Should().BeNull();
    }
}
