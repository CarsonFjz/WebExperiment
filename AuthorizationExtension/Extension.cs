using Microsoft.AspNetCore.Routing;
using System;
using System.Security.Claims;
using Basic.Core;

namespace Basic.AuthorizationExtension
{
    public static class Extension
    {
        public static string GetByKey(this ClaimsPrincipal claimsPrincipal, string key)
        {
            return claimsPrincipal.FindFirst(key)?.Value;
        }

        public static string GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetByKey(AuthenticatorKey.Role);
        }

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetByKey(AuthenticatorKey.UserId);
        }

        public static string GetPermission(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetByKey(AuthenticatorKey.Permission);
        }

        public static string GetControllerName(this RouteData routeData)
        {
            return routeData.Values["controller"].ToString();
        }

        public static string GetActionName(this RouteData routeData)
        {
            return routeData.Values["action"].ToString();
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool Verify(string permissionValue, RouteData routeData, string [] userPermission)
        {
            if (string.IsNullOrEmpty(permissionValue))
            {
                var currentAction = routeData.GetActionName();

                foreach (var item in userPermission)
                {
                    if (String.Equals(currentAction, item, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (var item in userPermission)
                {
                    if (String.Equals(permissionValue, item, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
