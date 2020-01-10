using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Basic.AuthorizationExtension.UserPermissionAuthorization
{
    public class UserPermissionCheckAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter, IAuthorizationRequirement
    {
        public UserPermissionOperation Permission { get; set; } = new UserPermissionOperation();

        public UserPermissionCheckAttribute(string permission = "")
        {
            if (string.IsNullOrEmpty(Permission.Name))
            {
                Permission = new UserPermissionOperation()
                {
                    Name = permission
                };
            }
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationService = context.HttpContext.RequestServices.GetService<IAuthorizationService>();

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
