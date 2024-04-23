using RentVilla.MVC.Models.Product;

namespace RentVilla.MVC.Services.HttpClientService
{
    public interface IHttpClientService
    {
        public Task<HttpResponseMessage> GetHttpResponse(string url);
        public Task<HttpResponseMessage> PostHttpRequest<T>(string url, T postModel);
        public Task<HttpResponseMessage> PutHttpRequest<T>(string url, T putModel);
        public Task<HttpResponseMessage> DeleteHttpRequest(string url);
    }
}
