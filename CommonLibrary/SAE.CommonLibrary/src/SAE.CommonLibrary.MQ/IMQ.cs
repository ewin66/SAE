using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.MQ
{
    /// <summary>
    /// 消息队列接口
    /// </summary>
    public interface IMQ:IDisposable
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        IMQ Publish<TMessage>(TMessage message);

        /// <summary>
        /// 订阅
        /// </summary>
        IMQ Subscibe<TMessage>(Action<TMessage> action);
    }
}
