using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using self = SAE.CommonLibrary.Trace;

namespace SAE.CommonLibrary.Trace.Http
{
    /// <summary>
    /// 跟踪处理
    /// </summary>
    public class TracingHandler : DelegatingHandler
    {
        //private readonly ITraceInjector _injector;
        private readonly string _serviceName;
        /// <summary>
        /// 实例化跟踪处理
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="httpMessageHandler">自定义处理</param>
        public TracingHandler(string serviceName, HttpMessageHandler httpMessageHandler = null)
        {
            _serviceName = serviceName;
            InnerHandler = httpMessageHandler ?? new HttpClientHandler { UseProxy = false };
        }

        //internal TracingHandler(ITraceInjector injector, string serviceName, HttpMessageHandler httpMessageHandler = null)
        //{
        //    _injector = injector;
        //    _serviceName = serviceName;
        //    InnerHandler = httpMessageHandler ?? new HttpClientHandler();
        //}
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="request">请求消息体</param>
        /// <param name="cancellationToken">线程取消标记</param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var clientTrace = new self.ClientTrace(_serviceName, request.Method.ToString()))
            {
                clientTrace.Apply(request.Headers, (c, key, value) => c.Add(key, value));
                //if (clientTrace.Trace != null)
                //{
                //    _injector.Inject(clientTrace.Trace, request.Headers, (c, key, value) => c.Add(key, value));
                //}
                return Task.Run(() => clientTrace.TracedActionAsync(base.SendAsync(request, cancellationToken)));
            }
        }

    }
}
