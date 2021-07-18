using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.IntegrationTests.Data;
using SocialEventManager.Tests.IntegrationTests.Fixtures;
using SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests
{
    public class RolesControllerTests : IntegrationTest
    {
        public RolesControllerTests(ApiWebApplicationFactory fixture)
          : base(fixture)
        {
        }

        [Theory]
        [InlineAutoData]
        public async Task CreateRole_Should_Return_Ok(ApplicationRole applicationRole)
        {
            applicationRole.Id = Guid.NewGuid().ToString();

            await Client.CreateAsync(ApiPathConstants.Roles, applicationRole);
            Assert.Single(RolesData.Instance.Roles.Where(r => r.Name == applicationRole.Name));
        }

        [Theory]
        [InlineAutoData]
        public async Task CreateRole_DuplicateRoleName_Should_Return_BadRequest(ApplicationRole applicationRole)
        {
            HttpClient client = Factory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services => services.AddTransient<IRolesRepository, InvalidRolesStub>()))
                .CreateClient(new WebApplicationFactoryClientOptions());

            (HttpStatusCode statusCode, string message) = await client.CreateAsyncWithError(ApiPathConstants.Roles, applicationRole);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            Assert.Equal(ExceptionConstants.DuplicateRoleName(applicationRole.Name), message);
        }

        [Theory]
        [MemberData(nameof(ApplicationRoleData.InvalidApplicationRole), MemberType = typeof(ApplicationRoleData))]
        public async Task CreateRole_InvalidData_Should_Return_BadRequest(ApplicationRole applicationRole, string expectedResult)
        {
            (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(ApiPathConstants.Roles, applicationRole);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            Assert.Equal(expectedResult, message);
        }
    }
}
