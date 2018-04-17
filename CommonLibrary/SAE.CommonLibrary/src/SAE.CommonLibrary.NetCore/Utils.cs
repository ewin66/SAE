using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.NetCore
{
    public static class Utils
    {
        /// <summary>
        /// 返回基目录
        /// </summary>
        /// <returns></returns>
        public static string BaseDirectory()
        {
#if !NETSTANDARD1_6
            return AppDomain.CurrentDomain.BaseDirectory;
#else
            return AppContext.BaseDirectory;
#endif
        }
    }
}
