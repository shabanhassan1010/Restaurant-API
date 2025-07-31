namespace Restaurant.API.Middlewares
{
    public class RateLimitingMiddleware   //  to check if the request try to access any endpoint more than 5 in 10 seconds
    {
        /// <summary>
        /// Middlewares ----> is work or use Scoped dependency Injection
        /// </summary>

        public static int _counter = 0;
        public static DateTime _LastrequestDate = DateTime.UtcNow;
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> logger;

        public RateLimitingMiddleware(RequestDelegate next , ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _counter++;
                if (DateTime.Now.Subtract(_LastrequestDate).Seconds > 10)
                {
                    _counter = 1;
                    _LastrequestDate = DateTime.UtcNow;
                    await _next(context);
                }
                else
                {
                    if (_counter > 5)
                    {
                        _LastrequestDate = DateTime.Now;
                        await context.Response.WriteAsync("Rate Limit Exceeded");
                    }
                    else
                    {
                        _LastrequestDate = DateTime.Now;
                        await _next(context);
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Someone went to hack user system");
            }
        }
    }
}
