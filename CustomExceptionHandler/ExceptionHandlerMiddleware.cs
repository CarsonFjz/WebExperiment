using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basic.CustomExceptionHandler.Infrastructure;

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
        /// <param name="customExceptionHandlers"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IEnumerable<ICustomExceptionHandler> customExceptionHandlers)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var exceptionName = ex.GetType().Name;

                //在抛出错误前执行逻辑
                await _exceptionOptions.OnException.Invoke(context, exceptionName, ex);

                //追踪底层错误
                while (ex.InnerException != null) ex = ex.InnerException;

                if (exceptionName == nameof(Exception))
                {
                    await context.HttpWriteAsync((int)HttpStatusCode.InternalServerError, ex.Message);
                }

                var handler = customExceptionHandlers.FirstOrDefault(x => x.Realize == exceptionName);

                if (handler != null)
                {
                    await handler.Excute(context, ex);
                }
                else
                {
                    await context.HttpWriteAsync((int)HttpStatusCode.InternalServerError, "exception handler not implementation");
                }
            }
        }
    }
}
