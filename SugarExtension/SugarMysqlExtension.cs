using Basic.Core;
using Basic.Core.ConfigurationExtension;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Basic.SugarExtension
{
    public static class SugarMysqlExtension
    {
        public static void AddSqlSugarUseMysql(this IServiceCollection services, string sqlConnection = null)
        {
            sqlConnection = sqlConnection.IsNullOrEmpty() ? MicAppConfiguration.GetConfigurationValue(SqlSugarConfig.Key) : sqlConnection;

            SqlSugarClient client = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = sqlConnection,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                IsShardSameThread = true
            });

            services.AddSqlSugar(client);
        }
    }
}
