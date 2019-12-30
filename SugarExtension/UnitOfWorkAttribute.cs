using Microsoft.AspNetCore.Mvc.Filters;
using SqlSugar;

namespace Basic.SugarExtension
{
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ((SqlSugarClient)context.HttpContext.RequestServices.GetService(typeof(SqlSugarClient))).BeginTran();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            SqlSugarClient service = (SqlSugarClient)context.HttpContext.RequestServices.GetService(typeof(SqlSugarClient));

            if (context.Exception == null)
            {
                service.CommitTran();
            }
            else
            {
                service.RollbackTran();
            }
        }
    }
}
