using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Basic.AuthorizationExtension.RoleAuthorization
{
    public class RoleCheckAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter, IAuthorizationRequirement
    {
        public RoleOperation FixedRoles { get; set; } = new RoleOperation();

        public RoleCheckAttribute(params string[] roles)
        {
            if (string.IsNullOrEmpty(FixedRoles.Name))
            {
                FixedRoles = new RoleOperation()
                {
                    Name = string.Join(",", roles)
                };
            }
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if ((await authorizationService.AuthorizeAsync(context.HttpContext.User, null, FixedRoles)).Succeeded)
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
