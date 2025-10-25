using System.Diagnostics;

namespace WebApplication17.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("Start {method} {path}", ctx.Request.Method, ctx.Request.Path);
            await _next(ctx);
            sw.Stop();
            _logger.LogInformation("End {method} {path} => {status} in {ms}ms",
                ctx.Request.Method, ctx.Request.Path, ctx.Response.StatusCode, sw.ElapsedMilliseconds);
        }
    }

    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<LoggingMiddleware>();
    }
}
