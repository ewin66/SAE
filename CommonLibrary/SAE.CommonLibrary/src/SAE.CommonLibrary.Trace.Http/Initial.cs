using SAE.CommonLibrary.Http;
using System.Net.Http;

namespace SAE.CommonLibrary.Trace.Http
{
   /// <summary>
   /// 初始化
   /// </summary>
    public class Initial
    {
        /// <summary>
        /// 初始化rpc远程调用，此函数将会覆盖Rpc远程调用组件的HpptClient
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="handle">自定义的处理Handle</param>
        public static void Init(string serviceName,HttpMessageHandler handle=null)
        {
            HttpRequestMessageExtension.Client = new HttpClient(
                                                 new TracingHandler(serviceName, handle));
        }
    }
}
