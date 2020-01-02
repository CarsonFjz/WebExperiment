using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basic.CapWithSugarExtension
{
    public interface ITranPublisher
    {
        Task PublishAsync<T>(string name, T contentObj, string callbackName = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
