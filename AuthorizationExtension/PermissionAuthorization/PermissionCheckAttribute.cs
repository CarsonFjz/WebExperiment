using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Basic.AuthorizationExtension.PermissionAuthorization
{
    public class PermissionCheckAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter, IAuthorizationRequirement
    {
        public PermissionOperation Permission { get; set; } = new PermissionOperation();

        public PermissionCheckAttribute(string permission = "")
        {
            if (string.IsNullOrEmpty(Permission.Name))
            {
                Permission = new PermissionOperation()
                {
                    Name = permission
                };
            }
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();

            context.HttpContext.GetRouteData();

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if ((await authorizationService.AuthorizeAsync(context.HttpContext.User, context.HttpContext.GetRouteData(), Permission)).Succeeded)
                {
                    await Task.CompletedTask;
                }
                else
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }

        }
    }
}
