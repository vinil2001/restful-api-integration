using Newtonsoft.Json;
using System.Text.Json;

namespace RestfullApiIntegration.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
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
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorResponse = new { message = "An error occurred.", details = ex.Message };
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));

            }
        }
    }

}
