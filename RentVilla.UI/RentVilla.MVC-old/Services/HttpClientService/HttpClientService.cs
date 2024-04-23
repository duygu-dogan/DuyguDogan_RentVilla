
using System.Text;
using System.Text.Json;

namespace RentVilla.MVC.Services.HttpClientService
{
    public class HttpClientService<T> : IHttpClientService<T> where T : class
    {
        private readonly IConfiguration _configuration;

        public HttpClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> DeleteHttpRequest(string url)
        {
            string baseUrl = _configuration["API:Url"];
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage responseApi = await httpClient.DeleteAsync(url);
                return responseApi;
            }
        }

        public async Task<T> GetHttpResponse(string url, T? instance)
        {
            string? baseUrl = _configuration["API:Url"];
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage responseApi = await httpClient.GetAsync(url);
                string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                instance = System.Text.Json.JsonSerializer.Deserialize<T>(contentResponseApi);

                if (instance == null)
                {
                    throw new NullReferenceException();
                }else
                {
                    return instance;
                }
            }
        }

        public async Task<HttpResponseMessage> PostHttpRequest(string url, T instance)
        {
            string baseUrl = _configuration["API:Url"];
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                string jsonContent = JsonSerializer.Serialize(instance);

                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage responseApi = await httpClient.PostAsync(url, content);
                return responseApi;
            }
        }

        public async Task<HttpResponseMessage> PutHttpRequest(string url, T instance)
        {
            string baseUrl = _configuration["API:Url"];
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                string jsonContent = JsonSerializer.Serialize(instance);

                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage responseApi = await httpClient.PutAsync(url, content);
                return responseApi;
            }
        }
    }
}
