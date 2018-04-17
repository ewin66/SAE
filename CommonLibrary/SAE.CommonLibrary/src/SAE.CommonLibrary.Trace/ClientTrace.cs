using System;
using zipkin4net;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport;

namespace SAE.CommonLibrary.Trace
{
    /// <summary>
    /// 客户端跟踪
    /// </summary>
    public class ClientTrace:BaseTrace
    {

        /// <summary>
        /// 创建一个客户端监控
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="operation">操作</param>
        public ClientTrace(string serviceName, string operation)
        {
            this.Trace = Unity.Get().Child();
            this.RecordEvent += () =>
            {
                this.Trace.Record(Annotations.ClientSend(), this._beginDate);
                this.Trace.Record(Annotations.ServiceName(serviceName), this._beginDate);
                this.Trace.Record(Annotations.Rpc(operation), this._beginDate);
                this.Trace.Record(Annotations.ClientAddr(SerializerUtils.DefaultEndPoint), this._beginDate);
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void ForceSampled()
        {
            Unity.Get().ForceSampled();
            base.ForceSampled();
        }


        /// <summary>
        /// 附加跟踪参数
        /// </summary>
        /// <typeparam name="TE">需要附加的类型</typeparam>
        /// <param name="carrier">载体</param>
        /// <param name="action">附加的方式</param>
        public void Apply<TE>(TE carrier, Action<TE, string, string> action)
        {
            var injector = new ZipkinHttpTraceInjector();
            injector.Inject(this.Trace, carrier, action);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            this.Trace.Record(Annotations.ClientRecv());
        }

    }
}
