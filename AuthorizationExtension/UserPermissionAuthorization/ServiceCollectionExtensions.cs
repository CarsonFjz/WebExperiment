﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Basic.AuthorizationExtension.UserPermissionAuthorization
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserPermissionsAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, UserPermissionAuthorizationHandler>();

            return services;
        }
    }
}
