using Basic.CustomExceptionHandler.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Basic.CustomExceptionHandler
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomExceptionHandler(this IServiceCollection services, params Type [] opts)
        {
            foreach (var item in opts)
            {
                services.AddSingleton(typeof(ICustomExceptionHandler), item);
            }

            services.AddTransient<IStartupFilter, ExceptionStartupFilter>();

            return services;
        }
    }
}
