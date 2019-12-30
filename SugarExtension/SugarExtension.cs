using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Basic.SugarExtension
{
    public static class SugarExtension
    {
        public static IServiceCollection AddSqlSugar(this IServiceCollection services, SqlSugarClient client)
        {
            services.AddSingleton<SqlSugarClient>(client);
            return services;
        }
    }
}
