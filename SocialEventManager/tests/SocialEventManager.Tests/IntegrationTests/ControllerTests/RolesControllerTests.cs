using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
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
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests
{
    [IntegrationTest]
    [Category(CategoryConstants.Identity)]
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
            RolesData.Instance.Roles.Should().ContainSingle(r => r.Name == applicationRole.Name);
        }

        [Theory]
        [InlineAutoData]
        public async Task CreateRole_DuplicateRoleName_Should_Return_BadRequest(ApplicationRole applicationRole)
        {
            HttpClient client = Factory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services => services.AddTransient<IRolesRepository, InvalidRolesStub>()))
                .CreateClient(new WebApplicationFactoryClientOptions());

            (HttpStatusCode statusCode, string message) = await client.CreateAsyncWithError(ApiPathConstants.Roles, applicationRole);
            statusCode.Should().Be(HttpStatusCode.BadRequest);
            message.Should().Be(ExceptionConstants.DuplicateRoleName(applicationRole.Name));
        }

        [Theory]
        [MemberData(nameof(ApplicationRoleData.InvalidApplicationRole), MemberType = typeof(ApplicationRoleData))]
        public async Task CreateRole_InvalidData_Should_Return_BadRequest(ApplicationRole applicationRole, string expectedResult)
        {
            (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(ApiPathConstants.Roles, applicationRole);
            statusCode.Should().Be(HttpStatusCode.BadRequest);
            message.Should().Be(expectedResult);
        }
    }
}
