using Basic.Core.ResultModel;
using Basic.CustomExceptionHandler;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebTest.Exceptions.ExceptionHandler
{
    /// <summary>
    /// 友好提示
    /// </summary>
    public class UserFriendlyExceptionHandler : BaseCustomExceptionHandler<UserFriendlyException>
    {
        public override async Task Excute(HttpContext context, Exception ex)
        {
            var exception = ex as UserFriendlyException;
            var result = new PublicResult<string>(exception.Code, exception.Message);
            await context.HttpWriteAsync((int)HttpStatusCode.Accepted, ex.Message);
        }
    }
}
