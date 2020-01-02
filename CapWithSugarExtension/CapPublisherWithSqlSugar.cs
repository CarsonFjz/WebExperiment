using System.Data;
using DotNetCore.CAP;
using SqlSugar;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Basic.CapWithSugarExtension
{
    public class CapPublisherWithSqlSugar : ITranPublisher
    {
        private readonly ICapPublisher _capBus;
        private readonly SqlSugarClient _sqlSugarClient;
        public CapPublisherWithSqlSugar(ICapPublisher capBus, SqlSugarClient sqlSugarClient)
        {
            _capBus = capBus;
            _sqlSugarClient = sqlSugarClient;
        }
        public async Task PublishAsync<T>(string name, T contentObj, string callbackName = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            _capBus.Transaction.Value = _capBus.ServiceProvider.GetService<ICapTransaction>();

            _capBus.Transaction.Value.JoinTransaction(_sqlSugarClient.Ado.Transaction);

            await _capBus.PublishAsync(name, contentObj, callbackName, cancellationToken);
        }
    }
}
