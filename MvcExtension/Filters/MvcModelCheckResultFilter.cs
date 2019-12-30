using Basic.MvcExtension.Tips;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Basic.Core.ResultModel;

namespace Basic.MvcExtension.Filters
{
    public class MvcModelCheckResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var sb = new StringBuilder();

                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        sb.AppendFormat("{0}", error.ErrorMessage, "|");
                    }
                }

                var errorModel = new Error(ModelBindingErrorTip.Code, sb.ToString());

                var result = new Result<Error>(errorModel);

                context.Result = new JsonResult(result);
            }
        }
    }
}
