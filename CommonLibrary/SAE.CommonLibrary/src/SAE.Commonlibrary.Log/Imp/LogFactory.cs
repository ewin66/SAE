using System;

namespace SAE.CommonLibrary.Log.Imp
{
    /// <summary>
    /// log工厂
    /// </summary>
    public class LogFactory : ILogFactory
    {
        private readonly string _logName;
        private readonly ILog _log;
        static LogFactory()
        {
            Unit.Init();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        public LogFactory(string logName = Unit.Default)
        {
            this._logName = logName;
            this._log = new Log(this._logName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ILog Create()
        {
            return this._log;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        public ILog Create(string logName)
        {
            
            return new Log(logName);
        }
        /// <summary>
        /// 根据创建log记录器
        /// </summary>
        /// <typeparam name="TCategoryName"></typeparam>
        /// <returns></returns>
        public ILog Create<TCategoryName>()
        {
            var logName=typeof(TCategoryName).FullName;

            return this.Create(logName);
        }
    }
}