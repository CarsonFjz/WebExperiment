using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace Basic.AuthorizationExtension.PermissionAuthorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionOperation, RouteData>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionOperation requirement, RouteData routeData)
        {
            var userPermissionTemp = context.User.GetPermission();

            if (!string.IsNullOrWhiteSpace(userPermissionTemp))
            {
                var userPermission = userPermissionTemp.Split(',');

                var permissionValue = requirement.Name;

                var isVerify = Extension.Verify(permissionValue, routeData, userPermission);

                if (isVerify)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
