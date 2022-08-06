using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace SocialEventManager.Shared.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T?> GetAndDeserializeAsync<T>(this HttpClient client, string requestUri) =>
        await client.GetFromJsonAsync<T>(requestUri);

    public static async Task<HttpResponseMessage> CreateAsync(this HttpClient client, string requestUri) =>
        await client.CreateAsync<object>(requestUri, null);

    public static async Task<HttpResponseMessage> CreateAsync<T>(this HttpClient client, string requestUri, T? obj)
    {
        using HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, obj);
        response.EnsureSuccessStatusCode();

        return response;
    }

    public static async Task<(HttpStatusCode StatusCode, string Message)> CreateAsyncWithError(this HttpClient client, string requestUri) =>
        await client.CreateAsyncWithError<object>(requestUri, null);

    public static async Task<(HttpStatusCode StatusCode, string Message)> CreateAsyncWithError<T>(this HttpClient client, string requestUri, T? obj)
    {
        using HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, obj);
        string message = await response.Content.ReadAsStringAsync();

        return (response.StatusCode, message);
    }

    public static async Task<TResult?> CreateAndDeserializeAsync<T, TResult>(this HttpClient client, string requestUri, T obj)
    {
        using HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, obj);
        response.EnsureSuccessStatusCode();

        string result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResult>(result);
    }
}
