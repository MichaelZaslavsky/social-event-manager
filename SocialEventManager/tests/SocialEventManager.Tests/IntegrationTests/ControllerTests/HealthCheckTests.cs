using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.IntegrationTests.Fixtures;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests
{
    [IntegrationTest]
    [Category(CategoryConstants.HealthChecks)]
    public class HealthCheckTests : IntegrationTest
    {
        public HealthCheckTests(ApiWebApplicationFactory fixture)
          : base(fixture)
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
}
