using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Data;
using System.Threading.Tasks;

namespace Basic.CapWithSugarExtension
{
    public class CapUnitOfWorkAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dbClient = context.HttpContext.RequestServices.GetRequiredService<SqlSugarClient>();

            var capBus = context.HttpContext.RequestServices.GetRequiredService<ICapPublisher>();

            using (var trans = dbClient.Context.Ado.Connection.BeginTransaction(capBus, autoCommit: false))
            {
                dbClient.Context.Ado.Transaction = (IDbTransaction)trans.DbTransaction;

                await next();

                trans.Commit();
            }
        }
    }
}
