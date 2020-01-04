using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using Microsoft.OpenApi.Models;

namespace Basic.SwaggerExtension
{
    public static class SwaggerExtension
    {
        public static void AddBasicSwagger(this IServiceCollection services, OpenApiInfo swaggerDoc = null)
        {
            OpenApiInfo info = swaggerDoc;

            if (info == null)
            {
                info = new OpenApiInfo()
                {
                    Title = "swagger",
                    Description = "swagger",
                    Version = "default"
                };
            }

            swaggerDoc = info;
            SwaggerParam.Name = swaggerDoc.Version;
            SwaggerParam.Title = swaggerDoc.Title;
            services.AddCustomSwagger(swaggerDoc);
            services.AddTransient<IStartupFilter, SwaggerStartupFilter>();
        }

        public static IApplicationBuilder UseSwaggerMiddleWare(
            this IApplicationBuilder app)
        {
            return app.UseCustomSwagger();
        }
    }

    internal sealed class SwaggerStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(
            Action<IApplicationBuilder> next)
        {
            return (Action<IApplicationBuilder>)(app =>
            {
                app.UseSwaggerMiddleWare();
                next(app);
            });
        }
    }
}
