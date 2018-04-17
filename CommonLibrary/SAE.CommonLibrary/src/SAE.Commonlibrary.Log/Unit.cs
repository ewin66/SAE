using SAE.CommonLibrary.Log.Imp;
using System;
using System.IO;

namespace SAE.CommonLibrary.Log
{

    internal class Unit
    {
        private static readonly object _lock = new object();
        private static bool IsInit = false;
        /// <summary>
        /// 默认的Log名称
        /// </summary>
        public const string Default = "Default";
        public const string ConfigPath = "Config/nlog.config";
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            if (!IsInit)
            {
                lock (_lock)
                {
                    if (!IsInit)
                    {
                        
                        //NLog.Targets.Target.Register<LogStashTarget>("LogStash");
                        var path = Path.Combine(
#if !NET45
                AppContext.BaseDirectory
#else
                AppDomain.CurrentDomain.BaseDirectory
#endif
                , ConfigPath);
                        NLog.Config.XmlLoggingConfiguration.SetCandidateConfigFilePaths(new string[] { path });
                        IsInit = true;
                    }
                }
            }
            
        }
    }
}
