using Basic.ErrorHandling;
using Basic.MvcExtension.Filters;
using Basic.SugarExtension;
using Basic.SwaggerExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace WebTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSqlSugarUseMysql("Server=rm-wz9a059sw5s0au6e29o.mysql.rds.aliyuncs.com;Port=3306;User ID=root;Password=RootXdf@124;DataBase=test1");

            services.AddMvc(opt =>
            {
                //ModelBinding统一处理
                opt.Filters.Add(typeof(MvcModelCheckResultFilter), 1);
                //返回统一格式参数
                opt.Filters.Add(typeof(MvcApiResultFilter), 2);

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddBasicSwagger(new Info()
            {
                Title = "WebFilter",
                Description = "WebFilter",
                Version = "v1.0.0"
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //处理错误要放到最前面，拦截所有的错误，mvc有自己的拦截机制，如果使用mvc拦截此中间件会无效
            app.UsePipelineErrorHandling();
            app.UseMvc();
        }
    }
}
