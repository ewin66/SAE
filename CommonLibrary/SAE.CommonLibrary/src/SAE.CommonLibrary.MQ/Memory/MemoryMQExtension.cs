using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.MQ;
using SAE.CommonLibrary.MQ.Memory;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// MQ扩展
    /// </summary>
    public static class MemoryMQExtension
    {
        /// <summary>
        /// 添加MQ内存实现
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryMQ(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IMQ, MemoryMQ>();
            serviceCollection.AddLogger();
            return serviceCollection;
        }

        /// <summary>
        /// 使用MQ设施
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IServiceProvider UseMQ(this IServiceProvider serviceProvider)
        {
            return serviceProvider.UseServiceFacade();
        }
    }
}