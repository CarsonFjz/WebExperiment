using Basic.CustomExceptionHandler;
using Basic.MvcExtension.Filters;
using Basic.SugarExtension;
using Basic.SwaggerExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddSqlSugarUseMysql("Server=rm-wz9a059sw5s0au6e29o.mysql.rds.aliyuncs.com;Port=3306;User ID=root;Password=RootXdf@124;DataBase=test1");

            services.AddControllers(opt =>
            {
                //ModelBinding统一处理
                opt.Filters.Add(typeof(MvcModelCheckResultFilter), 1);
                //返回统一格式参数
                opt.Filters.Add(typeof(MvcApiResultFilter), 2);

            });

            services.AddBasicSwagger(new Info()
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
