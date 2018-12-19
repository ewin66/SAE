using SAE.CommonLibrary.MQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Builder
{
    /// <summary>
    /// 构建者
    /// </summary>
    public interface IRegistrationBuilder
    {
        /// <summary>
        /// 映射集合
        /// </summary>
        IEnumerable<Map> Maps { get; }
        /// <summary>
        /// 添加一个映射对象
        /// </summary>
        /// <param name="map"></param>
        void Add(Map map);

        /// <summary>
        /// 开始构建
        /// </summary>
        void Build();
    }
}
