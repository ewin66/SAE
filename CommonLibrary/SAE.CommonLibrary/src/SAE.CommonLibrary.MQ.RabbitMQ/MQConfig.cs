using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.MQ.RabbitMQ
{
    /// <summary>
    /// RabbitMQ实现类
    /// </summary>
    public class MQConfig
    {
        public MQConfig()
        {
            this.AutoReset = true;
            this.Number = 256;
        }
        /// <summary>
        /// 应答模式
        /// </summary>
        public bool Ack { get => this.Number > 0; }
        /// <summary>
        /// 自动重试
        /// </summary>
        public bool AutoReset { get; set; }
        /// <summary>
        /// 队列处理的最大个数，默认为256
        /// </summary>
        public ushort Number { get; set; }
        /// <summary>
        /// 队列名称
        /// </summary>
        public string Query { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }  
    }
}
