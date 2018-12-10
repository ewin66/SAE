using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable
{
    /// <summary>
    /// 持久化服务,不关心存储的是什么数据结构,统一根据id来操作数据
    /// </summary>
    public interface IPersistenceService
    {
        T GetById<T>(string id) where T : class;
        void Add<T>(T t) where T : class;
        void Update<T>(T t) where T : class;
        void Remove<T>(T t) where T : class;
    }
}
