
namespace Restaurant.Api.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> _logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
