using System.Reflection;
using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

namespace SocialEventManager.Tests.Common.DependencyInjection;

public static class FakeRegistrationCollectionExtensions
{
    public static IServiceCollection RegisterFakes(this IServiceCollection services)
    {
        IEnumerable<Type> types = GetTypes();
        IDictionary<string, Type> fakeTypesByName = GetFakeTypesByName();

        foreach (Type type in types)
        {
            string? name = type.FullName?
                .TakeAfterLast(".")
                .TakeAfterFirst(GlobalConstants.InterfacePrefix)
                .TakeUntilLast(GlobalConstants.Repository);

            if (name is null)
            {
                return services;
            }

            if (fakeTypesByName.ContainsKey(name))
            {
                services.AddTransient(type, fakeTypesByName[name]);
            }
        }

        services.AddScoped<IEmailProvider, StubEmailSmtpProvider>();
        services.AddScoped<ISmtpClient, StubSmtpClient>();

        return services;
    }

    private static IEnumerable<Type> GetTypes()
    {
        Assembly dalAssembly = Assembly.Load($"{nameof(SocialEventManager)}.{nameof(DAL)}");
        return dalAssembly.GetTypes()
            .Where(t => t.IsInterface
                && t.Name.StartsWith(GlobalConstants.InterfacePrefix)
                && t.Name.EndsWith(GlobalConstants.Repository));
    }

    private static IDictionary<string, Type> GetFakeTypesByName()
    {
        Assembly testsAssembly = Assembly.Load($"{nameof(SocialEventManager)}.{nameof(Tests)}");
        return testsAssembly
            .GetTypes()
            .Where(t => t.IsClass && t.Name.StartsWith(GlobalConstants.Fake))
            .ToDictionary(t => t.Name.TakeAfterFirst(GlobalConstants.Fake));
    }
}
