using Basic.AuthorizationExtension.PermissionAuthorization;
using Basic.AuthorizationExtension.RoleAuthorization;
using Basic.AuthorizationExtension.UserPermissionAuthorization;
using Basic.Core.ConfigurationExtension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Basic.AuthorizationExtension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthorizationExtension(this IServiceCollection services)
        {
            var issuer = MicAppConfiguration.GetConfigurationValue("AuthenticationOption:Issuer");
            var audience = MicAppConfiguration.GetConfigurationValue("AuthenticationOption:Audience");
            var issuerSigningKey = MicAppConfiguration.GetConfigurationValue("AuthenticationOption:IssuerSigningKey");

            AuthenticationOption config = new AuthenticationOption
            {
                Issuer = issuer,
                Audience = audience,
                IssuerSigningKey = issuerSigningKey
            };

            return services.AddJwtAuthorizationExtension(x =>
            {
                x.Issuer = config.Issuer;
                x.Audience = config.Audience;
                x.IssuerSigningKey = config.IssuerSigningKey;
            });
        }
        public static IServiceCollection AddJwtAuthorizationExtension(this IServiceCollection services, Action<AuthenticationOption> configure)
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
