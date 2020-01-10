using Basic.CustomExceptionHandler.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Basic.CustomExceptionHandler
{
    public abstract class BaseCustomExceptionHandler<TException> : ICustomExceptionHandler where TException : Exception
    {
        public string Realize => typeof(TException).Name;

        /// <summary>
        /// 执行例外相关逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public abstract Task Excute(HttpContext context, Exception ex);
    }
}
