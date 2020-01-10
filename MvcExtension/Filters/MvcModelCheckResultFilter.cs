using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

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
                var errorArr = new ModelBindingErrorModel();

                foreach (var item in context.ModelState.Values)
                {
                    errorArr.Errors = new string[item.Errors.Count];

                    for (int i = 0; i < item.Errors.Count; i++)
                    {
                        errorArr.Errors[i] = item.Errors[i].ErrorMessage;
                    }
                }

                if (errorArr.Errors != null)
                {
                    var result = new JsonResult(errorArr)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };

                    context.Result = result;
                }
            }
        }
    }
}
