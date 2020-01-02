using Basic.Core.ResultModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basic.CustomExceptionHandler
{
    /// <summary>
    /// 处理管道错误的管道
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CustomExceptionHandlerOptions _exceptionOptions;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="exceptionOptions"></param>

        public ExceptionHandlerMiddleware(RequestDelegate next, IOptions<CustomExceptionHandlerOptions> exceptionOptions)
        {
            _next = next;
            _exceptionOptions = exceptionOptions.Value;
        }

        /// <summary>
        /// 拦截中间件错误
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //追踪底层错误
                while (ex.InnerException != null) ex = ex.InnerException;

                var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
                    .CreateLogger<CustomExceptionHandlerOptions>();

                var error = _exceptionOptions.OnException.Invoke(context, logger, ex);

                //返回错误
                var resultDto = new Result<Error>(error);

                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(resultDto));
            }
        }
    }
}
