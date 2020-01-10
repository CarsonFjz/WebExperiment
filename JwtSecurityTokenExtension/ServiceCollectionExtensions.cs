using Basic.Core;
using Basic.JwtSecurityTokenExtension.Implementation;
using Basic.JwtSecurityTokenExtension.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Basic.JwtSecurityTokenExtension
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

            if (config.IssuerSigningKey.Length <= 18)
            {
                throw new ArgumentException("密钥需要大于18位");
            }

            services.AddMemoryCache();

            services.TryAddScoped<IJwtSecurityToken, JwtSecurityToken>();

            services.TryAddScoped<IRefreshTokenStore, RefreshTokenStore>();

            services.AddSingleton(config);

            return services;
        }
    }
}
