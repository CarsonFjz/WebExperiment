using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Basic.SwaggerExtension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(
            this IServiceCollection services,
            Info swaggerDoc,
            ApiKeyScheme securityDefinition)
        {
            services.AddSwaggerGen((Action<SwaggerGenOptions>) (c =>
            {
                c.SwaggerDoc(SwaggerParam.Name, swaggerDoc);
                c.DocInclusionPredicate((Func<string, ApiDescription, bool>) ((docName, description) => true));
                c.DocumentFilter<LowerCaseDocumentFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                if (securityDefinition != null)
                {
                    c.AddSecurityDefinition(securityDefinition.Name, (SecurityScheme) securityDefinition);
                    Dictionary<string, IEnumerable<string>> dictionary = new Dictionary<string, IEnumerable<string>>()
                    {
                        {
                            securityDefinition.Name,
                            (IEnumerable<string>) new string[0]
                        }
                    };
                    c.AddSecurityRequirement((IDictionary<string, IEnumerable<string>>) dictionary);
                }

                foreach (string filePath in ServiceCollectionExtensions.GetAllFileByPath(AppContext.BaseDirectory,
                    "*.xml", new List<string>()))
                    c.IncludeXmlComments(filePath, false);
            }));
            return services;
        }

        private static List<string> GetAllFileByPath(
            string path,
            string search,
            List<string> filePaths)
        {
            if (!Directory.Exists(path))
                return filePaths;
            foreach (string file in Directory.GetFiles(path, search))
                filePaths.Add(file);
            foreach (string directory in Directory.GetDirectories(path))
                ServiceCollectionExtensions.GetAllFileByPath(directory, search, filePaths);
            return filePaths;
        }
    }
}
