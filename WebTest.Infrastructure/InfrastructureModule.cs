using AutoMapper;
using System;
using Volo.Abp.Modularity;

namespace WebTest.Infrastructure
{
    public class InfrastructureModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapper((Action<IServiceProvider, IMapperConfigurationExpression>)null, AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
