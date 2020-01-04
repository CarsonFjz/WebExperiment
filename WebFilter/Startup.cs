using Basic.CapWithSugarExtension;
using Basic.Core.ConfigurationExtension;
using Basic.CustomExceptionHandler;
using Basic.MvcExtension.Filters;
using Basic.SugarExtension;
using Basic.SwaggerExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebTest.Model;

namespace WebTest
{
    /// <summary>
    /// 启动
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSqlSugarUseMysql();

            services.AddCap(x =>
            {
                x.UseSqlSugar("Server=localhost;Port=3306;User ID=root;Password=942937;DataBase=my");
                x.UseRabbitMQ(opt =>
                {
                    opt.HostName = "localhost";
                    opt.UserName = "guest";
                    opt.Password = "guest";
                    opt.Port = 5672;
                });
                x.UseDashboard();
                x.FailedRetryCount = 1;
                x.ConsumerThreadCount = 1;
            });

            services.AddControllers(opt =>
            {
                //ModelBinding统一处理
                opt.Filters.Add(typeof(MvcModelCheckResultFilter), 1);
                //返回统一格式参数
                opt.Filters.Add(typeof(MvcApiResultFilter), 2);

            }).AddNewtonsoftJson();

            services.AddBasicSwagger(new OpenApiInfo()
            {
                Title = "WebFilter",
                Description = "WebFilter",
                Version = "v1.0.0"
            });
        }

        /// <summary>
        /// 管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app)
        {
            //处理错误要放到最前面，拦截所有的错误，mvc有自己的拦截机制，如果使用mvc拦截此中间件会无效
            app.UseCustomExceptionHandler();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
