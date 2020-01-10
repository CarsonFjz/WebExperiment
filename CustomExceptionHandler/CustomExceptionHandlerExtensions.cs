using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

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

    public class ExceptionStartupFilter : IStartupFilter
    {
        /// <summary>
        /// 配置后台工作进程启动
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseCustomExceptionHandler();

                next(app);
            };
        }
    }
}
