using Basic.Core;
using Basic.Core.ConfigurationExtension;
using Basic.SugarExtension;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Basic.CapWithSugarExtension
{
    public static class CapOptionsExtensions
    {
        public static CapOptions UseSqlSugar(this CapOptions options, string connectionString = null)
        {
            connectionString = connectionString.IsNullOrEmpty()
                ? MicAppConfiguration.GetConfigurationValue(SqlSugarConfig.Key)
                : connectionString;

            return options.UseMySql((Action<MySqlOptions>)(opt => opt.ConnectionString = connectionString));
        }
    }
}
