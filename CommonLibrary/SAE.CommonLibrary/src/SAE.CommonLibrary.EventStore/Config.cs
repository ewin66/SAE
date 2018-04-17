using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 快照存储的区间默认是10
        /// </summary>
        public static Func<int> SnapshotInterval = () => 10;
    }
}
