using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Basic.CustomExceptionHandler
{
    public class CustomExceptionHandlerOptions
    {
        /// <summary>
        /// 自定义错误执行前
        /// </summary>
        public Func<HttpContext, string, Exception, Task> OnException { get; set; } =
            async (context, exName, exception) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
                    .CreateLogger<Exception>();

                logger.LogError(exception.GetHashCode(), exception, "system error");

                await Task.CompletedTask;
            };

    }
}
