using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Basic.SugarExtension
{
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var service = context.HttpContext.RequestServices.GetRequiredService<SqlSugarClient>();

            service.BeginTran();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            SqlSugarClient service = context.HttpContext.RequestServices.GetRequiredService<SqlSugarClient>();

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
