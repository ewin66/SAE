using SAE.CommonLibrary.Log;
using SAE.CommonLibrary.Log.Imp;
using System.Collections.Generic;
using zipkin4net;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport;
using zipkin4net.Transport.Http;
using tracing = zipkin4net;

namespace SAE.CommonLibrary.Trace
{
    /// <summary>
    /// 默认的跟踪提供程序
    /// </summary>
    internal class TraceProvider : ITraceProvider
    {

        readonly ITraceExtractor _extractor;
        /// <summary>
        /// 通过跟踪配置初始化一个跟踪
        /// </summary>
        /// <param name="option"></param>
        public TraceProvider(TraceOptions option)
        {
            //创建默认log器
            var log = new LogFactory();

            var logger = new TraceLogger(new Log<TraceLogger>(log));
            //创建发送实例
            var sender = new HttpZipkinSender(option.Endpoint, "application/json");

            logger.LogInformation($"{nameof(option.Endpoint)}:{option.Endpoint}");

            TraceManager.SamplingRate = option.SamplingRate;

            logger.LogInformation($"{nameof(option.SamplingRate)}:{option.SamplingRate}");
            //创建跟踪
            var tracer = new ZipkinTracer(new RateLimiterZipkinSender(sender,option.MaxRequest,option.Duration), new JSONSpanSerializer());
            //注册跟踪
            TraceManager.RegisterTracer(tracer);
            //开始
            TraceManager.Start(logger);

            _extractor = new ZipkinHttpTraceExtractor();
        }

        /// <summary>
        /// 通过<paramref name="dic"/>构建本地跟踪通过<see cref="ServerTrace"/>,<see cref="LocalTrace"/>使用
        /// </summary>
        /// <param name="dic">字典对象</param>
        public void Create(IDictionary<string, string> dic = null)
        {
            if (dic == null)
            {
                Unity.Get();
            }
            else
            {
                tracing.Trace trace;
                if (!_extractor.TryExtract(dic, (c, key) => 
                {
                    string value;
                    c.TryGetValue(key, out value);
                    return value;
                }, out trace))
                {
                    Unity.Get();
                }
                else
                {
                    Unity.Set(trace);
                }
            }
        }

        /// <summary>
        /// 停止跟踪
        /// </summary>
        public void Dispose()
        {
            TraceManager.Stop();
        }


    }

}
