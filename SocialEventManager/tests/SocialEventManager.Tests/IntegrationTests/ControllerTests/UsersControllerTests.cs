using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Newtonsoft.Json;
using SocialEventManager.BLL.Models;
using SocialEventManager.Shared.Constants;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests
{
    // Temp class - for test purposes
    [IntegrationTest]
    [Category(CategoryConstants.User)]
    public class UsersControllerTests
    {
        [Theory]
        [InlineAutoData]
        public async Task CreateUser(UserDto user)
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44322/api/users");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string json = JsonConvert.SerializeObject(user);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("6F483588-B3BE-4C70-9A1D-B98F6755E968")]
        public async Task GetUser(Guid id)
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:44322/api/users/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
