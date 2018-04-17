using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.EventStore.Snapshot;
using SAE.EventStore.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Document.Memory
{
    /// <summary>
    /// 
    /// </summary>
    public static class MemoryExtensnion
    {
        /// <summary>
        /// 添加EventStore.Docment.Memory默认实现
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemberDocument(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddJson()
                             .AddDocument();
            
            serviceCollection.TryAddSingleton<ISnapshotStore, MemorySnapshotStore>();
            serviceCollection.TryAddSingleton<ISnapshotStore, MemorySnapshotStore>();
            serviceCollection.TryAddSingleton<IEventStore, MemoryEventStore>();
            serviceCollection.TryAddSingleton<IDocumentEvent, MemoryDocumentEvent>();

            return serviceCollection;
        }
    }
}
