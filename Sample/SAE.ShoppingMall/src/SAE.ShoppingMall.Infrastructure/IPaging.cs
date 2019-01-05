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
        int PageIndex { get; }

        /// <summary>
        /// 容量
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount { get; }
        /// <summary>
        /// 总个数
        /// </summary>
        long TotalNumber { get; set; }
        
    }
}
