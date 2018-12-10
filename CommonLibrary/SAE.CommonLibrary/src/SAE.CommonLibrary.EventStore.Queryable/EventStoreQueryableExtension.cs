using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.EventStore.Queryable.Default;

namespace SAE.CommonLibrary.EventStore.Queryable
{
    public static class EventStoreQueryableExtension
    {
        /// <summary>
        /// 添加内存形式的存储对象
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryPersistenceService(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IPersistenceService, MemoryPersistenceService>();
            
            return serviceCollection;
        }

        /// <summary>
        /// 添加默认的转让服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultTransferService(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IAssignmentService, TransferService>();

            return serviceCollection;
        }
    }
}
