using Restaurant.Domain.Exepections;

namespace Restaurant.API.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        #region Constructor
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            this.logger = logger;
        }
        #endregion
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundExepection notFound)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(notFound.Message);
                logger.LogWarning(notFound.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something wrong happened");
            }
        }
    }
}
