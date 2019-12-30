using Microsoft.AspNetCore.Builder;

namespace Basic.ErrorHandling
{
    public static class ErrorHandlingExtensions
    {
        /// <summary>
        /// 拓展
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePipelineErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
