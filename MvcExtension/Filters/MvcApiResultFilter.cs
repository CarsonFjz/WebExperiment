using Basic.Core.ResultModel;
using Basic.MvcExtension.Tips;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
                    var error = new Error(NotFoundResultTip.Code, NotFoundResultTip.Value);

                    var result = new Result<object>(error);

                    context.Result = new JsonResult(result);
                }
                else
                {
                    var result = new Result<object>(((ObjectResult)context.Result).Value);

                    context.Result = new JsonResult(result);
                }
            }
            else if (context.Result is EmptyResult)
            {
                var result = new Result<object>(true);

                context.Result = new JsonResult(result);
            }
            else if (context.Result is ContentResult)
            {
                var result = new Result<object>((context.Result as ContentResult).Content);

                context.Result = new JsonResult(result);
            }
            else if (context.Result is StatusCodeResult)
            {
                var result = new Result<object>((context.Result as StatusCodeResult).StatusCode);

                context.Result = new JsonResult(result);
            }
        }
    }
}
