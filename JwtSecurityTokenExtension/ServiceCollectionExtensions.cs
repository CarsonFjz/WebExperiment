using Basic.Core;
using Basic.JwtSecurityTokenExtension.Implementation;
using Basic.JwtSecurityTokenExtension.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Basic.JwtSecurityTokenExtension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtTokenExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var issuer = configuration.GetSection("JwtOption:Issuer").Value;
            var audience = configuration.GetSection("JwtOption:Audience").Value;
            var issuerSigningKey = configuration.GetSection("JwtOption:IssuerSigningKey").Value;
            var accessTokenExpiresMinutes = configuration.GetSection("JwtOption:AccessTokenExpiresMinutes").Value;
            var refreshTokenExpiresMinutes = configuration.GetSection("JwtOption:RefreshTokenExpiresMinutes").Value;

            JwtOption config = new JwtOption
            {
                Issuer = issuer,
                Audience = audience,
                IssuerSigningKey = issuerSigningKey,
                AccessTokenExpiresMinutes = Convert.ToInt32(accessTokenExpiresMinutes),
                RefreshTokenExpiresMinutes = Convert.ToInt32(refreshTokenExpiresMinutes)
            };

            return services.AddJwtTokenExtension(x =>
            {
                x.Issuer = config.Issuer;
                x.Audience = config.Audience;
                x.IssuerSigningKey = config.IssuerSigningKey;
                x.AccessTokenExpiresMinutes = config.AccessTokenExpiresMinutes;
                x.RefreshTokenExpiresMinutes = config.RefreshTokenExpiresMinutes;
            });
        }

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
