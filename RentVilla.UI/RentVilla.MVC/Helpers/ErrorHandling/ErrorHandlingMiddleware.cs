using Newtonsoft.Json;
using RentVilla.MVC.Models;

namespace RentVilla.MVC.Helpers.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string routeWhereExceptionIs = context.Request.Path;
                var path = JsonConvert.SerializeObject(routeWhereExceptionIs);
                var result = new ErrorViewModel
                {
                    Path = path
                };

                if (ex is AggregateException ae)
                {
                    var messages = ae.InnerExceptions.Select(e => e.Message).ToList();
                    result.ErrorMessages = messages;
                }
                else
                {
                    string message = ex.Message;
                    result.ErrorMessages = new List<string> { message };
                }

                string messageJson = JsonConvert.SerializeObject(result);
                context.Items["ErrorMessageJson"] = messageJson;

                await HandleErrorAsync(context);
            }
        }

        private static async Task HandleErrorAsync(HttpContext context)
        {
            string messagesJson = context.Items["ErrorMessageJson"] as string;
            string redirectUrl = $"/Error?messagesJson={messagesJson}";
            context.Response.Redirect(redirectUrl);
        }
    }
}
