using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.EventStore.Queryable.Default;
using SAE.CommonLibrary.EventStore.Queryable.Handle;

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
        /// 添加基于<seealso cref="IStorage"/><seealso cref="IPersistenceService"/>实现
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddStoragePersistenceService(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IPersistenceService, StoragePersistenceService>();
            return serviceCollection;
        }


        /// <summary>
        /// 添加默认处理器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultHandler(this IServiceCollection services)
        {
            services.TryAddScoped(typeof(DefaultAddHandler<,>), typeof(DefaultAddHandler<,>));
            services.TryAddScoped(typeof(DefaultUpdateHandler<,>), typeof(DefaultUpdateHandler<,>));
            services.TryAddScoped(typeof(DefaultRemoveHandler<,>), typeof(DefaultRemoveHandler<,>));
            return services;
        }
    }
}
