using zipkin4net;
using zipkin4net.Tracers.Zipkin;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using zipkin = zipkin4net;

namespace SAE.CommonLibrary.Trace
{
    /// <summary>
    /// 服务跟踪
    /// </summary>
    public class ServerTrace:BaseTrace
    {

        /// <summary>
        /// 创建一个服务跟踪
        /// </summary>
        /// <param name="serviceName">对应服务的名称</param>
        /// <param name="port">端口号</param>
        public ServerTrace(string serviceName, int port)
        {
            this.Trace = Unity.Get();

            this.RecordEvent += () =>
            {
                this.Trace.Record(Annotations.ServerRecv(), this._beginDate);
                this.Trace.Record(Annotations.ServiceName(serviceName), this._beginDate);
                this.Trace.Record(Annotations.ServerAddr(serviceName,
                                  new IPEndPoint(SerializerUtils.DefaultEndPoint.Address, port)),
                                  this._beginDate);
            };


        }

        /// <summary>
        /// 创建一个服务跟踪
        /// </summary>
        /// <param name="serviceName">对应服务的名称</param>
        /// <param name="operation">对应的操作</param>
        /// <param name="port">端口号</param>
        public ServerTrace(string serviceName,string operation,int port=80) :this(serviceName, port)
        {

            this.RecordEvent += () =>
            {
                this.Trace.Record(Annotations.Rpc(operation), this._beginDate);
            };
        }

        /// <summary>
        /// 结束service跟踪
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            this.Trace.Record(Annotations.ServerSend());
        }


    }

}
