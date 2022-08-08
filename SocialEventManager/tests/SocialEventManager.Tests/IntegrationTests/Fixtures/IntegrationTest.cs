using System.Reflection;
using System.Text;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures;

public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
{
    public IntegrationTest(ApiWebApplicationFactory fixture)
    {
        Factory = fixture;
        Client = Factory.CreateClient();

        byte[] byteArray = Encoding.ASCII.GetBytes("TempUser:TempPassword");
        Client.DefaultRequestHeaders.Authorization = new(AuthConstants.Scheme, Convert.ToBase64String(byteArray));

        InitStorages();
    }

    protected ApiWebApplicationFactory Factory { get; }

    protected HttpClient Client { get; }

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
