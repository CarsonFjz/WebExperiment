using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Basic.AuthorizationExtension.UserPermissionAuthorization
{
    public class UserPermissionAuthorizationHandler : AuthorizationHandler<UserPermissionOperation, RouteData>
    {
        private readonly IUserPermissionStore _userPermissionStore;

        public UserPermissionAuthorizationHandler(IUserPermissionStore userPermissionStore = null)
        {
            _userPermissionStore = userPermissionStore;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserPermissionOperation requirement, RouteData routeData)
        {
            if (_userPermissionStore != null)
            {
                var userPermission = _userPermissionStore.GetUserPermission(context.User.GetUserId());

                if (userPermission.Length > 0)
                {
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
            }
            else
            {
                throw new NotImplementedException("you need Implemented IUserPermissionStore");
            }

            return Task.CompletedTask;
        }
    }
}
