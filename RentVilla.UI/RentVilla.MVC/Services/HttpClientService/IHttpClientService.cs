namespace RentVilla.MVC.Services.HttpClientService
{
    public interface IHttpClientService<T>
    {
        public Task<T> GetHttpResponse(string url, T instance);
        public Task<HttpResponseMessage> PostHttpRequest(string url, T instance);
        public Task<HttpResponseMessage> PutHttpRequest(string url, T instance);
        public Task<HttpResponseMessage> DeleteHttpRequest(string url);
    }
}
