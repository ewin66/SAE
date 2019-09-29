using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.Trace;
using System;
using zipkin4net;
using local = SAE.CommonLibrary.Trace;
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 跟踪扩展
    /// </summary>
    public static class TraceApplicationBuilderExtensions
    {
        private static ITraceProvider provider;

        /// <summary>
        /// 添加跟踪
        /// </summary>
        /// <param name="build"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseTrace(this IApplicationBuilder build)
        {
            
            return build.UseTrace(s=> { });
        }

        /// <summary>
        /// 添加跟踪
        /// </summary>
        /// <param name="build"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseTrace(this IApplicationBuilder build,Action<TraceOptions> action)
        {
            var option = new TraceOptions();

            action(option);

            var env = build.ApplicationServices.GetService<IHostingEnvironment>();

            var lifetime = build.ApplicationServices.GetService<IApplicationLifetime>();

            //注册应用启动停止事件
            lifetime.ApplicationStarted.Register(() =>
            {
                provider = TraceBuild.Create(option);
            });

            lifetime.ApplicationStopped.Register(() => provider?.Dispose());
            //添加具体的跟踪
            build.Use(async (context, next) =>
            {
                var dic = new System.Collections.Generic.Dictionary<string, string>();

                var request = context.Request;

                foreach (var kv in request.Headers)
                {
                    dic.Add(kv.Key, kv.Value);
                }
                provider?.Create(dic);
                using (var serverTrace = new local.ServerTrace(env.ApplicationName, 
                                                              request.Method,
                                                              request.Host.Port ?? 80))
                {
                    serverTrace.Record("http.host", request.Host.ToString())
                               .Record("http.uri", $"{request.Scheme}://{request.Host}{request.Path}")
                               .Record("http.path", request.Path);

                    await serverTrace.TracedActionAsync(next());
                }
            });


            return build;
        }

    }
}