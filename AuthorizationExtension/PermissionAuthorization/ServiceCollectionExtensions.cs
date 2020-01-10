using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Basic.AuthorizationExtension.PermissionAuthorization
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPermissionsAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            return services;
        }
    }
}
