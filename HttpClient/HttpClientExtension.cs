using Microsoft.Extensions.DependencyInjection;

namespace Basic.HttpClient
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddSqlSugar(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IHttpClient, StandardHttpClientFactory>();

            return services;
        }
    }
}
