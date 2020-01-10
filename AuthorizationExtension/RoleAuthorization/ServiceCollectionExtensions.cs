using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Basic.AuthorizationExtension.RoleAuthorization
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRoleAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, RolePermissionsAuthorizationHandler>();

            return services;
        }
    }
}
