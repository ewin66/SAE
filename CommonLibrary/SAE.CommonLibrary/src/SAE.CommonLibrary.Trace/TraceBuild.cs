

using System;
using System.Collections;
using System.Collections.Generic;
using tracing = zipkin4net;

namespace SAE.CommonLibrary.Trace
{
    /// <summary>
    /// 跟踪构建者
    /// </summary>
    public class TraceBuild
    {
        private readonly static object _lock = new object();
        private static ITraceProvider provider;
        /// <summary>
        /// 构建一个跟踪提供程序
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public static ITraceProvider Create(TraceOptions option)
        {
            lock (_lock)
            {
                if (provider == null)
                    provider = new TraceProvider(option);
            }
            
            return provider;
        }
    }
}
