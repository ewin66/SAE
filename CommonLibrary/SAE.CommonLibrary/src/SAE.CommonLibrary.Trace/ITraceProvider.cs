using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.Trace
{
    /// <summary>
    /// 跟踪提供程序
    /// </summary>
    public interface ITraceProvider:IDisposable
    {
        /// <summary>
        /// 通过<paramref name="dic"/>构建本地跟踪通过<see cref="ServerTrace"/>,<see cref="LocalTrace"/>使用
        /// </summary>
        /// <param name="dic">字典对象</param>
        void Create(IDictionary<string, string> dic=null);
    }
}
