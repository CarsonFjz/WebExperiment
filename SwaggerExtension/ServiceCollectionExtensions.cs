using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;

namespace Basic.SwaggerExtension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, OpenApiInfo swaggerDoc)
        {
            services.AddSwaggerGen((Action<SwaggerGenOptions>) (option =>
            {
                option.SwaggerDoc(SwaggerParam.Version, swaggerDoc);

                option.DocInclusionPredicate((Func<string, ApiDescription, bool>) ((docName, description) => true));

                option.DocumentFilter<LowerCaseDocumentFilter>();

                #region 给接口加入Authorize
                //添加公共Authorize
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    //把token放在头部
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization"
                });

                //每个接口添加锁
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            //把token放在头部
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                #endregion

                #region 读取根目录下的xml注释赋值到接口注释
                var filePaths = new List<string>();

                filePaths = ServiceCollectionExtensions.GetAllFileByPath(AppContext.BaseDirectory, "*.xml", filePaths);

                foreach (var filePath in filePaths)
                {
                    option.IncludeXmlComments(filePath, false);
                } 
                #endregion
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
