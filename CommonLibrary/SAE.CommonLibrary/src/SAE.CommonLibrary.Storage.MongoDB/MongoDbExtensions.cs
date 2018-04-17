#if !NET45
using SAE.CommonLibrary.Storage;
using SAE.CommonLibrary.Storage.MongoDB;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class MongoDbExtensions
    {
        /// <summary>
        /// 添加Storage
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="config">mongodb配置</param>
        /// <returns></returns>
        public static IServiceCollection AddStorage(this IServiceCollection serviceCollection,MongoDBConfig config=null)
        {
            if (config != null)
            {
                serviceCollection.TryAddSingleton(config);
            }
            serviceCollection.TryAddSingleton<IStorage, MongoDBStorage>();
            serviceCollection.AddJson()
                             .AddLogger();
            return serviceCollection;
        }

        
    }
}
#endif