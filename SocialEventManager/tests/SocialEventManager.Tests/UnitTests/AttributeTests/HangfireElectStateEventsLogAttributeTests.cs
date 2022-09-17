using FluentAssertions;
using Hangfire.States;
using SocialEventManager.Infrastructure.Filters.BackgroundJobs;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Mocks;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.AttributeTests;

[UnitTest]
[Category(CategoryConstants.Attributes)]
public sealed class HangfireElectStateEventsLogAttributeTests
{
    [Fact]
    public void OnStateElection_Should_NotThrowException_When_CandidateStateIsSuccees()
    {
        HangfireElectStateEventsLogAttribute electStateEventsLog = new();
        ElectStateContextMock context = new();

        Exception? executingException = Record.Exception(() => electStateEventsLog.OnStateElection(context.Object));
        executingException.Should().BeNull();
    }

    [Fact]
    public void OnStateElection_Should_NotThrowException_When_CandidateStateIsFailed()
    {
        HangfireElectStateEventsLogAttribute electStateEventsLog = new();
        ElectStateContextMock context = new();
        context.Object.CandidateState = new FailedState(new());

        Exception? executingException = Record.Exception(() => electStateEventsLog.OnStateElection(context.Object));
        executingException.Should().BeNull();
    }
}
