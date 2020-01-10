using Basic.AuthorizationExtension.PermissionAuthorization;
using Basic.AuthorizationExtension.RoleAuthorization;
using Basic.AuthorizationExtension.UserPermissionAuthorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basic.AuthorizationExtension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExtensionAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var issuer = configuration.GetSection("AuthenticationOption:Issuer").Value;
            var audience = configuration.GetSection("AuthenticationOption:Audience").Value;
            var issuerSigningKey = configuration.GetSection("AuthenticationOption:IssuerSigningKey").Value;
            var authenticateScheme = configuration.GetSection("AuthenticationOption:AuthenticateScheme").Value;

            AuthenticationOption config = new AuthenticationOption
            {
                Issuer = issuer,
                Audience = audience,
                IssuerSigningKey = issuerSigningKey
            };

            return services.AddExtensionAuthorization(x =>
            {
                x.Issuer = config.Issuer;
                x.Audience = config.Audience;
                x.IssuerSigningKey = config.IssuerSigningKey;
            });
        }
        public static IServiceCollection AddExtensionAuthorization(this IServiceCollection services, Action<AuthenticationOption> configure)
        {
            AuthenticationOption config = new AuthenticationOption();

            configure(config);

            var sign = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.IssuerSigningKey)), "HS256");

            var tokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = sign.Key,
                ValidateIssuer = true,
                ValidIssuer = config.Issuer,
                ValidateAudience = true,
                ValidAudience = config.Audience,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true, //是否验证过期时间
                ClockSkew = TimeSpan.Zero //这个如果不填在某些情况下验证时间会不正确
            };

            //启用JWT AddAuthentication
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "Bearer";
                        options.DefaultChallengeScheme = "Bearer";
                    }).
                    AddJwtBearer(option =>
                    {
                        option.TokenValidationParameters = tokenValidationParameters;
                    });

            services.AddPermissionsAuthorization();

            services.AddUserPermissionsAuthorization();

            services.AddRoleAuthorization();

            return services;
        }
    }
}
