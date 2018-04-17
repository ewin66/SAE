﻿#if !NET45
using SAE.CommonLibrary.Log;
using SAE.CommonLibrary.Log.Imp;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// 添加Log默认实现
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddLogger(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<ILogFactory, LogFactory>();
            serviceCollection.TryAddSingleton(typeof(ILog<>),typeof(Log<>));
            
            return serviceCollection;
        }
    }

}
#endif