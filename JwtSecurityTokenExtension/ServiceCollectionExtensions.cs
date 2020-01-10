using Basic.Core;
using JwtSecurityTokenExtension.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using JwtSecurityTokenExtension.Infrastructure;

namespace JwtSecurityTokenExtension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtTokenExtension(this IServiceCollection services, Action<JwtOption> configure)
        {
            JwtOption config = new JwtOption();

            configure(config);

            if (config.IssuerSigningKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException("密钥不能为空");
            }

            if (config.IssuerSigningKey.Length <= 36)
            {
                throw new ArgumentException("密钥需要大于36位");
            }

            services.AddScoped<IJwtSecurityTokenExtension, Implementation.JwtSecurityTokenExtension>();

            services.TryAddSingleton<IRefreshTokenStore, RefreshTokenStore>();

            services.AddSingleton(config);

            return services;
        }
    }
}
