using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using SocialEventManager.Shared.Constants;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures
{
    public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            Factory = fixture;
            Client = Factory.CreateClient();

            byte[] byteArray = Encoding.ASCII.GetBytes("TempUser:TempPassword");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthConstants.Scheme, Convert.ToBase64String(byteArray));
        }

        protected ApiWebApplicationFactory Factory { get; }

        protected HttpClient Client { get; }
    }
}
