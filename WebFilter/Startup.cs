using Basic.AuthorizationExtension;
using Basic.CapWithSugarExtension;
using Basic.CustomExceptionHandler;
using Basic.JwtSecurityTokenExtension;
using Basic.MvcExtension.Filters;
using Basic.SugarExtension;
using Basic.SwaggerExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebTest.Exceptions.ExceptionHandler;

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
            //处理错误要放到最前面，拦截所有的错误，mvc有自己的拦截机制，如果使用mvc拦截此中间件会无效
            //这里一般会用来拦截非MVC的其他中间件的错误，把控到返回前端的数据
            //可以传递多个参数，以指定拦截不同的异常类型
            services.AddCustomExceptionHandler(typeof(UserFriendlyExceptionHandler));

            //配置验证
            services.AddJwtAuthorizationExtension();
            //替换获取用户权限信息实现
            //services.AddSingleton<IUserPermissionStore, UserPermissionStore>();

            //配置jwt
            services.AddJwtTokenExtension(Configuration);

            //配置SqlSugar
            services.AddSqlSugarUseMysql();

            //配置Cap
            services.AddCap(x =>
            {
                //使用SqlSugar配置
                x.UseSqlSugar();
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
                //模型绑定允许空
                opt.AllowEmptyInputInBodyModelBinding = true;
                //ModelBinding统一处理
                opt.Filters.Add(typeof(MvcModelCheckResultFilter), 1);
                //返回统一格式参数
                opt.Filters.Add(typeof(MvcApiResultFilter), 2);
                //业务友好提示处理，抛出200
                opt.Filters.Add(typeof(UserFriendlyExceptionFilterAttribute));

            });

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
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            //启动授权认证
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
