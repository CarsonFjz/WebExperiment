using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Basic.CustomExceptionHandler.Infrastructure
{
    public interface ICustomExceptionHandler
    {
        string Realize { get; }
        /// <summary>
        /// 执行处理错误逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        Task Excute(HttpContext context, Exception ex);
    }
}
