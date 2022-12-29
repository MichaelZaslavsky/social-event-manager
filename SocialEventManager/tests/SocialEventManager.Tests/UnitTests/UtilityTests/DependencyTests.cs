using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.API.Utilities;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.DependencyInjection;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.UtilityTests;

[Collection(TestConstants.StorageDependent)]
[UnitTest]
[Category(CategoryConstants.Utilities)]
public class DependencyTests
{
    [Theory]
    [MemberData(nameof(DependencyData.ValidDependencies), MemberType = typeof(DependencyData))]
    public void RegistrationValidation_Should_ReturnSuccess_When_DependenciesAreValid(
        IList<(Type ServiceType, Type? ImplementationType, ServiceLifetime Lifetime)> descriptors)
    {
        WebApplicationFactory<IApiMaker> app = new WebApplicationFactory<IApiMaker>()
            .WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(serviceCollection =>
                {
                    IList<ServiceDescriptor> services = serviceCollection.ToList();
                    DependencyAssertionResult actual = ValidateServices(services, descriptors);
                    actual.Success.Should().BeTrue();
                }));

        app.CreateClient();
        app.Dispose();
    }

    [Theory]
    [MemberData(nameof(DependencyData.InvalidDependencies), MemberType = typeof(DependencyData))]
    public void RegistrationValidation_Should_ReturnFailure_When_DependenciesAreInvalid(
        IList<(Type ServiceType, Type? ImplementationType, ServiceLifetime Lifetime)> descriptors, string expectedMessage)
    {
        WebApplicationFactory<IApiMaker> app = new WebApplicationFactory<IApiMaker>()
            .WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(serviceCollection =>
                {
                    IList<ServiceDescriptor> services = serviceCollection.ToList();
                    DependencyAssertionResult actual = ValidateServices(services, descriptors);
                    actual.Success.Should().BeFalse();
                    actual.Message.Should().Be(expectedMessage);
                }));

        app.CreateClient();
        app.Dispose();
    }

    private static DependencyAssertionResult ValidateServices(
        IList<ServiceDescriptor> services,
        IList<(Type ServiceType, Type? ImplementationType, ServiceLifetime Lifetime)> descriptors)
    {
        bool searchFailed = false;
        StringBuilder message = new();

        foreach ((Type serviceType, Type? implementationType, ServiceLifetime lifetime) in descriptors)
        {
            ServiceDescriptor? match = services.SingleOrDefault(
                s => s.ServiceType == serviceType
                && s.Lifetime == lifetime
                && s.ImplementationType == implementationType);

            if (match is not null)
            {
                continue;
            }

            if (!searchFailed)
            {
                message.Append(TestConstants.FailedToFindRegisteredServices);
                searchFailed = true;
            }

            message
                .Append(serviceType.Name)
                .Append('|')
                .Append(implementationType?.Name)
                .Append('|')
                .Append(lifetime)
                .AppendLine();
        }

        return new(!searchFailed, message.ToString());
    }
}
