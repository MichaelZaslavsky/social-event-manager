// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.Net;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using SocialEventManager.Tests.IntegrationTests.Fixtures;
using SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests;

[Collection(TestConstants.StorageDependent)]
[IntegrationTest]
[Category(CategoryConstants.Identity)]
public class RolesControllerTests : IntegrationTest
{
    public RolesControllerTests(ApiWebApplicationFactory fixture)
      : base(fixture)
    {
    }

    [Theory]
    [AutoData]
    public async Task CreateRole_Should_ReturnOk_When_RoleIsValid(ApplicationRole applicationRole)
    {
        applicationRole.Id = Guid.NewGuid().ToString();
        int initialCount = RolesStorage.Instance.Data.Count;

        await Client.CreateAsync(ApiPathConstants.Roles, applicationRole);

        RolesStorage.Instance.Data.Should().HaveCount(++initialCount)
            .And.ContainSingle(r => r.Name == applicationRole.Name);
    }

    [Theory]
    [AutoData]
    public async Task CreateRole_Should_ReturnBadRequest_When_RoleNameIsDuplicated(ApplicationRole applicationRole)
    {
        List<Role> initial = RolesStorage.Instance.Data;

        HttpClient client = Factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services => services.AddTransient<IRolesRepository, StubInvalidRoles>()))
                .CreateClient(new());

        (HttpStatusCode statusCode, string message) = await client.CreateAsyncWithError(ApiPathConstants.Roles, applicationRole);
        statusCode.Should().Be(HttpStatusCode.BadRequest);
        message.Should().Be(ExceptionConstants.DuplicateRoleName(applicationRole.Name));

        RolesStorage.Instance.Data.Should().BeEquivalentTo(initial);
    }

    [Theory]
    [MemberData(nameof(ApplicationRoleData.InvalidApplicationRole), MemberType = typeof(ApplicationRoleData))]
    public async Task CreateRole_Should_ReturnBadRequest_When_DataIsInvalid(ApplicationRole applicationRole, string expectedResult)
    {
        List<Role> initial = RolesStorage.Instance.Data;

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(ApiPathConstants.Roles, applicationRole);
        statusCode.Should().Be(HttpStatusCode.BadRequest);
        message.Should().Be(expectedResult);

        RolesStorage.Instance.Data.Should().BeEquivalentTo(initial);
    }
}
*/
