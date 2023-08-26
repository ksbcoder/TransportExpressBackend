using System.Net;
using System.Text.Json;
using TransportExpress.Wrappers;

namespace TransportExpress.Middlewares
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = new ResponseModel<string>() { Success = false, Message = ex?.Message, StatusCode = ex?.StatusCode};

                switch (ex)
                {
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}