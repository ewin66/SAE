using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.Trace
{
    /// <summary>
    /// 跟踪配置
    /// </summary>
    public class TraceOptions
    {
        /// <summary>
        /// 初始化默认配置
        /// </summary>
        public TraceOptions()
        {
            this.Endpoint = "http://zipkin.SAE.cs:9411";
#if RELEASE
            this.SamplingRate = 0.01f;
#else
            this.SamplingRate = 0.01f;
#endif
            this.Duration = TimeSpan.FromMinutes(1);
            this.MaxRequest = 100;
        }
        /// <summary>
        /// 跟踪终端
        /// </summary>
        public string Endpoint
        {
            get; set;
        }
        /// <summary>
        /// 采样率
        /// </summary>
        public float SamplingRate
        {
            get;
            set;
        }
        /// <summary>
        /// 最大请求数
        /// </summary>
        public int MaxRequest
        {
            get;set;
        }
        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan Duration
        {
            get;set;
        }
    }
}
