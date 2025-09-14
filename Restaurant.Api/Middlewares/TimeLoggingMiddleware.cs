
namespace Restaurant.Api.Middlewares;

public class TimeLoggingMiddleware(ILogger<TimeLoggingMiddleware> _logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var timeStarting = DateTime.Now;
        await next(context);
        var timeEnded = DateTime.Now;
        var timeTaken = timeEnded - timeStarting;
        _logger.LogInformation($"Time Taken: {timeTaken.TotalMilliseconds} ms");
        if ((timeTaken.TotalSeconds) > 4)
        {
            string path = context.Request.Path;
            string httpMethod = context.Request.Method;
            _logger.LogWarning($"Slow Request: {httpMethod} {path} took {timeTaken} s");
        }
    }
}
