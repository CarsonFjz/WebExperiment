using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Basic.AuthorizationExtension.RoleAuthorization
{
    public class RolePermissionsAuthorizationHandler : AuthorizationHandler<RoleOperation>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleOperation requirement)
        {
            if (context.User != null)
            {
                var roles = requirement.Name.Split(',');

                foreach (var role in roles)
                {
                    var rol = context.User.GetRole();

                    if (rol == role)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
