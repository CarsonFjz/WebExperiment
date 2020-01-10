using Basic.Core.ResultModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Basic.MvcExtension.Filters
{
    public class MvcApiResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;

                if (objectResult.Value == null)
                {
                    throw new Exception("no ObjectResult");
                }
                else
                {
                    var result = new PublicResult<object>(((ObjectResult)context.Result).Value);

                    context.Result = new JsonResult(result);
                }
            }
        }
    }
}
