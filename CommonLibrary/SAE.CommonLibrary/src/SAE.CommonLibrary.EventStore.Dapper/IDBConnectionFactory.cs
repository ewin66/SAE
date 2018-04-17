using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Document.Dapper
{
    public interface IDbConnectionFactory
    {
        
        /// <summary>
        /// 通过<paramref name="name"/>获得对应的数据库连接
        /// </summary>
        /// <param name="name">数据库连接名</param>
        /// <returns></returns>
        IDbConnection Get(string name);
    }
}
