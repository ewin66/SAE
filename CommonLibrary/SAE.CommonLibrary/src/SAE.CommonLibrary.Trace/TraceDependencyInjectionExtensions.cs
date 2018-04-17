#if !NET45
using zipkin4net;
using SAE.CommonLibrary.Trace;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 跟踪组件依赖扩展
    /// </summary>
    public static class TraceDependencyInjectionExtensions
    {
        /// <summary>
        /// 添加跟踪记录
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddTrace(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ILogger, TraceLogger>()
                             .AddLogger();
            return serviceCollection;
        }
    }
}
#endif