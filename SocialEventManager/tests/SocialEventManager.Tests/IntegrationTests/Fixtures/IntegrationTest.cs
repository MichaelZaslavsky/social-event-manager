using System.Net.Http;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures
{
    public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            Factory = fixture;
            Client = Factory.CreateClient();
        }

        protected ApiWebApplicationFactory Factory { get; }

        protected HttpClient Client { get; }
    }
}
