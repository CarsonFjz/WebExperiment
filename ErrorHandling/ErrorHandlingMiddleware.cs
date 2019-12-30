using Basic.Core.ResultModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basic.ErrorHandling
{
    /// <summary>
    /// 处理管道错误的管道
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
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
                Error error;

                if (ex is UserFriendlyException)
                {
                    //处理业务逻辑友好提示
                    error = HandlingUserFriendlyException(ex as UserFriendlyException);
                }
                else
                {
                    //追踪底层错误
                    while (ex.InnerException != null) ex = ex.InnerException;

                    error = HandlingException(ex);
                }

                //返回错误
                var resultDto = new Result<Error>(error);

                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(resultDto));
            }
        }

        public Error HandlingUserFriendlyException(UserFriendlyException ex)
        {
            return new Error(ex.BusinessCode, ex.Message);
        }

        public Error HandlingException(Exception ex)
        {
            return new Error(PipeLineErrorTip.Code, ex.Message, ex.StackTrace);
        }
    }
}
