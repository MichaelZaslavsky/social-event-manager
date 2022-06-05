using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace SocialEventManager.Shared.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T> GetAndDeserializeAsync<T>(this HttpClient client, string requestUri) =>
        await client.GetFromJsonAsync<T>(requestUri);

    public static async Task CreateAsync<T>(this HttpClient client, string requestUri, T obj)
    {
        using HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, obj);
        response.EnsureSuccessStatusCode();
    }

    public static async Task<(HttpStatusCode statusCode, string message)> CreateAsyncWithError<T>(this HttpClient client, string requestUri, T obj)
    {
        using HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, obj);
        string message = await response.Content.ReadAsStringAsync();

        return (response.StatusCode, message);
    }

    public static async Task<TResult> CreateAndDeserializeAsync<T, TResult>(this HttpClient client, string requestUri, T obj)
    {
        using HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, obj);
        response.EnsureSuccessStatusCode();
        string result = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<TResult>(result);
    }
}
