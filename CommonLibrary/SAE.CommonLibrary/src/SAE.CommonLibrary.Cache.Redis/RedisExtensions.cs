#if !NET45
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.Cache;
using SAE.CommonLibrary.Cache.Redis;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RedisExtensions
    {
        /// <summary>
        /// 添加Cache默认实现
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection serviceCollection,RedisConfig config=null)
        {
            if (config != null)
            {
                serviceCollection.TryAddSingleton(config);
            }
            serviceCollection.TryAddSingleton<ICache, RedisCache>();
            serviceCollection.AddJson()
                             .AddLogger();
            return serviceCollection;
        }
    }
}
#endif