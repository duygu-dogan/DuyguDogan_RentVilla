
using RentVilla.MVC.Models.Product;
using RentVilla.MVC.Services.TokenCookieService;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RentVilla.MVC.Services.HttpClientService
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenCookieHandlerService _tokenService;

        public HttpClientService(IConfiguration configuration, ITokenCookieHandlerService tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<HttpResponseMessage> DeleteHttpRequest(string url)
        {
            string? baseUrl = _configuration["API:Url"];
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                var accessToken = _tokenService.GetAccessToken();
                if (accessToken != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                HttpResponseMessage responseMessage = await httpClient.DeleteAsync(url);
                return responseMessage;
            }
        }

        public async Task<HttpResponseMessage> GetHttpResponse(string url)
        {
            string? baseUrl = _configuration["API:Url"];
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                var accessToken = _tokenService.GetAccessToken();
                if(accessToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                HttpResponseMessage responseMessage = await httpClient.GetAsync(url);
                return responseMessage;
            }
        }

        public async Task<HttpResponseMessage> PostHttpRequest<T>(string url, T postModel)
        {
            string baseUrl = _configuration["API:Url"];
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                string jsonContent = JsonSerializer.Serialize(postModel);

                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var accessToken = _tokenService.GetAccessToken();
                if (accessToken != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage responseMessage = await httpClient.PostAsync(url, content);
                return responseMessage;
            }
        }

        public async Task<HttpResponseMessage> PutHttpRequest<T>(string url, T putModel)
        {
            string baseUrl = _configuration["API:Url"];
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                string jsonContent = JsonSerializer.Serialize(putModel);

                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var accessToken = _tokenService.GetAccessToken();
                if (accessToken != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage responseMessage = await httpClient.PutAsync(url, content);
                return responseMessage;
            }
        }
    }
}
