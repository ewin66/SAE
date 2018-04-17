#if !NET45
using SAE.CommonLibrary.Trace.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// rpc跟踪ApplicationBuilder扩展
    /// </summary>
    public static class TraceRpcApplicationBuilderExtensions
    {
        /// <summary>
        /// 使用rpc跟踪
        /// </summary>
        /// <param name="app"></param>
        /// <param name="handler">自定义的处理</param>
        /// <returns></returns>
        public static IApplicationBuilder UseTraceRpc(this IApplicationBuilder app, HttpMessageHandler handler = null)
        {
            var hostingEnvironment = app.ApplicationServices.GetService<IHostingEnvironment>();
            Initial.Init(hostingEnvironment.ApplicationName, handler);
            return app;
        }
        
    }
}
#endif