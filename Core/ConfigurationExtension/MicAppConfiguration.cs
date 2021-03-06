﻿using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Basic.Core.ConfigurationExtension
{
    public static class MicAppConfiguration
    {
        public static Dictionary<string, object> Configurations = new Dictionary<string, object>();
        public static string GetConfigurationValue(string configName)
        {
            string key = ApplicationBaseInfo.Name + configName;

            if (Configurations.TryGetValue(key, out object configValue) == false)
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile(ApplicationBaseInfo.AppSettingPath, optional: true, reloadOnChange: false);

                IConfiguration configs = builder.Build();

                if (configs != null)
                {
                    foreach (var item in configs.AsEnumerable())
                    {
                        Configurations.Add(item.Key, item.Value);
                    }
                }

                return Configurations[key].ToString();
            }

            return configValue?.ToString();
        }
    }
}
