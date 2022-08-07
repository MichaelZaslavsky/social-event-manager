using FluentAssertions;
using Hangfire.States;
using SocialEventManager.Infrastructure.Filters.BackgroundJobs;
using SocialEventManager.Tests.Common.Mocks;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.FilterTests;

public class HangfireElectStateEventsLogAttributeTests
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
