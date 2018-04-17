#if !NET45
using SAE.CommonLibrary.Trace.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 跟踪链Rpc注入扩展
    /// </summary>
    public static class TraceRpcDependencyInjectionExtensions
    {
        ///// <summary>
        ///// 添加跟踪处理
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="handle"></param>
        ///// <returns></returns>
        //public static IServiceCollection AddTraceHandle(this IServiceCollection services, HttpMessageHandler handle=null)
        //{
        //    services.AddScoped<HttpMessageHandler>(provider =>
        //    {
        //        var env = provider.GetService<IHostingEnvironment>();

        //        return new TracingHandler(env.ApplicationName, handle);
        //    });

        //    return services;
        //}

        /// <summary>
        /// 添加跟踪HttpClient
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static IServiceCollection AddTraceClient(this IServiceCollection services, HttpMessageHandler handle = null)
        {
            services.AddSingleton(provider =>
            {
                var env = provider.GetService<IHostingEnvironment>();

                return new HttpClient(new TracingHandler(env.ApplicationName, handle));
            });

            return services;
        }
    }
}
#endif