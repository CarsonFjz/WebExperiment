using System.Net;
using Basic.Core.ResultModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Basic.MvcExtension.Filters
{
    public class UserFriendlyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is UserFriendlyException)
            {
                context.Result = new JsonResult(context.Exception.Message)
                {
                    StatusCode = (int) HttpStatusCode.OK
                };
            }
        }
    }
}
