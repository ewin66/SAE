using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPaging
    {
        /// <summary>
        /// 索引
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 容量
        /// </summary>
        int Size { get; }

        /// <summary>
        /// 总长度
        /// </summary>
        long Count { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        int Total { get; }
    }
}
