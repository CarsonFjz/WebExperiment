using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Basic.Core.Configuration
{
    public static class MicAppConfiguration
    {
        public static TConfig GetConfigurationModel<TConfig>(this IServiceCollection services) where TConfig : new()
        {
            var option = new TConfig();
            return option;
        }
    }
}
