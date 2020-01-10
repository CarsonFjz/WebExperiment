using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Basic.AuthorizationExtension
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder StartAuthentication(this IApplicationBuilder app)
        {
            //启用认证中间件
            app.UseAuthentication();

            return app;
        }
    }

    public class AuthenticationFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.StartAuthentication();

                next(app);
            };
        }
    }
}
