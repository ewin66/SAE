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
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public void Assert(bool condition, string message)
        {
            _log.Assert(condition, message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Write(string message)
        {
            _log.Write(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        public void Write(string message, MessageType messageType)
        {
            _log.Write(message, messageType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <param name="ex"></param>
        public void Write(string message, MessageType messageType, Exception ex)
        {
            _log.Write(message, messageType, ex);
        }
    }
}
