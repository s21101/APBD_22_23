using Cw9.Middlewares;

namespace Cw9.Extensions
{
    public static class ErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
