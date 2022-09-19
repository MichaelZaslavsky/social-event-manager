using System.Net;
using FluentAssertions;
using SocialEventManager.Infrastructure.Auth;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.IntegrationTests.Fixtures;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests;

[Collection(TestConstants.StorageDependent)]
[IntegrationTest]
[Category(CategoryConstants.HealthChecks)]
public sealed class HealthCheckTests : IntegrationTest
{
    public HealthCheckTests(ApiWebApplicationFactory fixture, IJwtHandler jwtHandler)
      : base(fixture, jwtHandler)
    {
    }

    [Theory]
    [InlineData(ApiPathConstants.HealthReady)]
    [InlineData(ApiPathConstants.HealthLive)]
    public async Task HealthCheck_Should_ReturnOkStatusCode_When_ApplicationIsHealthy(string requestUri)
    {
        HttpResponseMessage response = await Client.GetAsync(requestUri);
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
