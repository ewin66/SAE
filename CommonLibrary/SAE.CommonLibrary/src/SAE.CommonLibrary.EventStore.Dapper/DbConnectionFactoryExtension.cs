using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Document.Dapper
{
    public static class DbConnectionFactoryExtension
    {
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <returns></returns>
        public static IDbConnection Get(this IDbConnectionFactory dbConnectionFactory)
        {
            return dbConnectionFactory.Get("Default");

        }
    }
}
