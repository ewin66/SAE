using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.Provider
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    public class ServiceFacade
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        public static IServiceProvider Provider
        {
            get;
            internal set;
        }
    }
}
