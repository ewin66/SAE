﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.EventStore.Snapshot
{
    public interface ISnapshotStore
    {
        /// <summary>
        /// 根据id和版本号从快照中查找聚合对象
        /// </summary>
        /// <param name="identity">标识</param>
        /// <param name="version">返回版本的快照</param>
        /// <returns></returns>
        Task<Snapshot> FindAsync(IIdentity identity,long version);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="identity">标识</param>
        /// <returns></returns>
        Task<Snapshot> FindAsync(IIdentity identity);
        /// <summary>
        /// 保存快照
        /// </summary>
        /// <param name="snapshot">要保存的快照对象</param>
        Task SaveAsync(Snapshot snapshot);
    }
}
