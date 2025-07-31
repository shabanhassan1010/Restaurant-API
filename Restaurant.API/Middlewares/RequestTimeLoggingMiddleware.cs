using System.Diagnostics;

namespace Restaurant.API.Middlewares
{
    public class RequestTimeLoggingMiddleware : IMiddleware
    {
        #region Constructor
        private readonly ILogger<RequestTimeLoggingMiddleware> logger;
        public RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger)
        {
            this.logger = logger;
        }
        #endregion
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Start Timer
            Stopwatch stopwatch = Stopwatch.StartNew();
            await next.Invoke(context);
            // Finish Timer
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds / 1000 > 0)
            {
                logger.LogInformation("Request [{Verb}] at {path} took {time} ms", 
                    context.Request.Method , context.Request.Path , stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
