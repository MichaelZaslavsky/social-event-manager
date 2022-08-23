using System.Reflection;
using SocialEventManager.Infrastructure.Auth;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures;

public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
{
    protected const string Email = TestConstants.ValidEmail;

    public IntegrationTest(ApiWebApplicationFactory fixture, IJwtHandler jwtHandler)
    {
        JwtHandler = jwtHandler;
        Factory = fixture;
        Client = Factory.CreateClient();

        InitStorages();
        SetAuthorization(Email);
    }

    protected IJwtHandler JwtHandler { get; }

    protected ApiWebApplicationFactory Factory { get; }

    protected HttpClient Client { get; }

    protected void SetAuthorization(string email)
    {
        Client.DefaultRequestHeaders.Authorization = new(AuthConstants.Bearer, JwtHandler.GenerateToken(email));
    }

    private static void InitStorages()
    {
        IEnumerable<Type> storages = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                t.BaseType?.IsGenericType == true
                && (t.BaseType.GetGenericTypeDefinition() == typeof(ListStorage<,>) || t.BaseType.GetGenericTypeDefinition() == typeof(DictionaryStorage<,,>)));

        foreach (Type storage in storages)
        {
            PropertyInfo instance = storage.GetProperty(
                nameof(StorageBase<object>.Instance), BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)!;

            object? value = instance.GetValue(storage, null);

            storage.GetMethod(nameof(StorageBase<object>.Init))?
                .Invoke(value, null);
        }
    }
}
