using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SAE.CommonLibrary.MQ.Memory
{
    /// <summary>
    /// 基于内存的mq
    /// </summary>
    public class MemoryMQ : IMQ
    {
        private readonly MethodInfo _methodInfo;
        public MemoryMQ()
        {
            this._methodInfo = typeof(MQExtension).GetMethod("Get", BindingFlags.Static | BindingFlags.Public);
        }
        public void Dispose()
        {
            
        }

        public IMQ Publish<TMessage>(TMessage message)
        {
            var method = this._methodInfo.MakeGenericMethod(message.GetType());
            var @delegate = method.Invoke(null, new object[] { this }) as Delegate;
            @delegate.DynamicInvoke(message);
            return this;
        }

        public IMQ Subscibe<TMessage>(Action<TMessage> action)
        {
            this.Add(action);
            return this;
        }

    }
}
