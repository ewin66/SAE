using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.Log
{
    /// <summary>
    /// 日志适配器
    /// </summary>
    /// <typeparam name="TCategoryName"></typeparam>
    public class Log<TCategoryName> : ILog<TCategoryName>
    {
        private readonly ILog _log;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public Log(ILogFactory factory)
        {
            _log = factory.Create<TCategoryName>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsEnabled(Level level)
        {
            return this._log.IsEnabled(level);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public ILog Write(string message, Level level, Exception exception)
        {
           return this._log.Write(message, level, exception);
        }
    }
}
