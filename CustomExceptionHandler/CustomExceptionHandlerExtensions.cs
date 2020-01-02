using Microsoft.AspNetCore.Builder;

namespace Basic.CustomExceptionHandler
{
    public static class CustomExceptionHandlerExtensions
    {
        /// <summary>
        /// 拓展
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
