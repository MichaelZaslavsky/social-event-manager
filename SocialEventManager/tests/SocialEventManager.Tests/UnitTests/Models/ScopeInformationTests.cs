using System.Reflection;
using FluentAssertions;
using SocialEventManager.Infrastructure.Loggers;
using SocialEventManager.Shared.Constants;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.Models;

public sealed class ScopeInformationTests
{
    [Fact]
    public void Init_Should_Succeed_When_Called()
    {
        ScopeInformation scopeInfo = new();

        scopeInfo.HostScopeInfo.Keys.Should().Contain(GlobalConstants.MachineName);
        scopeInfo.HostScopeInfo[GlobalConstants.MachineName].Should().Be(Environment.MachineName);

        scopeInfo.HostScopeInfo.Keys.Should().Contain(GlobalConstants.EntryPoint);
        scopeInfo.HostScopeInfo[GlobalConstants.EntryPoint].Should().Be(Assembly.GetEntryAssembly()?.GetName().Name!);
    }
}
