using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Basic.SwaggerExtension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, OpenApiInfo swaggerDoc)
        {
            services.AddSwaggerGen((Action<SwaggerGenOptions>) (c =>
            {
                c.SwaggerDoc(SwaggerParam.Name, swaggerDoc);

                c.DocInclusionPredicate((Func<string, ApiDescription, bool>) ((docName, description) => true));

                c.DocumentFilter<LowerCaseDocumentFilter>();

                var filePaths = new List<string>();

                filePaths = ServiceCollectionExtensions.GetAllFileByPath(AppContext.BaseDirectory, "*.xml", filePaths);

                foreach (var filePath in filePaths)
                {
                    c.IncludeXmlComments(filePath, false);
                }
            }));

            return services;
        }

        private static List<string> GetAllFileByPath(string path, string search, List<string> filePaths)
        {
            if (!Directory.Exists(path))
            {
                return filePaths;
            }

            foreach (var file in Directory.GetFiles(path, search))
            {
                filePaths.Add(file);
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                //递归获取子文件夹
                ServiceCollectionExtensions.GetAllFileByPath(directory, search, filePaths);
            }
                
            return filePaths;
        }
    }
}
