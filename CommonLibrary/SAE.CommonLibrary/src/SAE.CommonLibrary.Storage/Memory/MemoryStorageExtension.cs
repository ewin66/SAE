using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.Storage;
using SAE.CommonLibrary.Storage.Memory;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class MongoDbExtensions
    {
        /// <summary>
        /// 添加内存Storage,只限于测试使用
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryStorage(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IStorage, MemoryStorage>();
            serviceCollection.AddLogger();
            return serviceCollection;
        }
        
    }
}