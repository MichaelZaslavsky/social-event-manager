using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SocialEventManager.Shared.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAndDeserializeAsync<T>(this HttpClient client, string requestUri) =>
            await client.GetFromJsonAsync<T>(requestUri);

        public static async Task<HttpResponseMessage> CreateAsync<T>(this HttpClient client, string requestUri, T obj)
        {
            using HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, obj);
            response.EnsureSuccessStatusCode();

            return response;
        }

        public static async Task<TResult> CreateAndDeserializeAsync<T, TResult>(this HttpClient client, string requestUri, T obj)
        {
            using HttpResponseMessage response = await client.CreateAsync(requestUri, obj);
            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(result);
        }
    }
}
