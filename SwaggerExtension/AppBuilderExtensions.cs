using Microsoft.AspNetCore.Builder;
using System;

namespace Basic.SwaggerExtension
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{SwaggerParam.Version}/swagger.json", SwaggerParam.Title);
            });

            return app;
        }
    }
}
