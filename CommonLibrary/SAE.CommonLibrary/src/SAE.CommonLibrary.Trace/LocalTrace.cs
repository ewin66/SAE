using zipkin4net;
using zipkin = zipkin4net;

namespace SAE.CommonLibrary.Trace
{
    /// <summary>
    /// 本地跟踪
    /// </summary>
    public class LocalTrace : BaseTrace
    {
        
        /// <summary>
        /// 开启本地操作
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rpc"></param>
        public LocalTrace(string name,string rpc=null)
        {
            this.Trace = zipkin.Trace.Current.Child();
            this.RecordEvent += () =>
            {
                this.Trace.Record(Annotations.LocalOperationStart(name));
                this.Trace.Record(Annotations.WireRecv(), this._beginDate);
                this.Trace.Record(Annotations.ServiceName(name));
                if (!string.IsNullOrWhiteSpace(rpc))
                    this.Trace.Record(Annotations.Rpc(rpc));
            };
        }


        /// <summary>
        /// 强制发布
        /// </summary>
        protected override void ForceSampled()
        {
            Unity.Get().ForceSampled();
            base.ForceSampled();
        }
        /// <summary>
        /// 停止本地操作
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            this.Trace.Record(Annotations.WireSend());
            this.Trace.Record(Annotations.LocalOperationStop());
        }

       
    }
}
