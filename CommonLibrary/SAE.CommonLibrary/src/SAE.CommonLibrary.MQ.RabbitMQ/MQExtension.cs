#if NETSTANDARD2_0
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.MQ;
using SAE.CommonLibrary.MQ.RabbitMQ;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// MQ扩展
    /// </summary>
    public static class MQExtension
    {
        /// <summary>
        /// 添加MQ默认实现
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="config">mq配置</param>
        /// <returns></returns>
        public static IServiceCollection AddMQ(this IServiceCollection serviceCollection,MQConfig config=null)
        {
            serviceCollection.AddLogger()
                             .AddJson()
                             .TryAddSingleton<IMQ, MQ>();
            if (config != null)
            {
                serviceCollection.TryAddSingleton(config);
            }
            return serviceCollection;
        }
    }
}

#endif