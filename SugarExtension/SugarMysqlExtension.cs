using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Basic.SugarExtension
{
    public static class SugarMysqlExtension
    {
        public static void AddSqlSugarUseMysql(this IServiceCollection services, string connectionString)
        {
            SqlSugarClient client = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                IsShardSameThread = true
            });

            services.AddSqlSugar(client);
        }
    }
}
