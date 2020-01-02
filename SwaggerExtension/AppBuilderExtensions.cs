using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
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

            app.UseSwagger((Action<SwaggerOptions>) (c =>
                    c.PreSerializeFilters.Add((Action<SwaggerDocument, HttpRequest>) ((swagger, httpReq) => swagger.Host = httpReq.Host.Value))))
                .UseSwaggerUI((Action<SwaggerUIOptions>) (c =>
                    c.SwaggerEndpoint("/swagger/" + SwaggerParam.Name + "/swagger.json", SwaggerParam.Title + " " + SwaggerParam.Name)));
            
            return app;
        }
    }
}
