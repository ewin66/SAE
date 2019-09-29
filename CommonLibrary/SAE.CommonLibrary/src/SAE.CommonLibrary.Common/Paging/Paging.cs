using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Paging : IPaging
    {
        /// <summary>
        /// 默认页数
        /// </summary>
        protected const int DefaultIndex = 1;
        /// <summary>
        /// 默认容量
        /// </summary>
        protected const int DefaultSize = 10;
        /// <summary>
        /// 
        /// </summary>
        public Paging()
        {
            this.pageIndex = DefaultIndex;
            this.PageSize = DefaultSize;
        }
        private int pageIndex;
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex
        {
            get { return this.pageIndex; }
            set
            {
                this.pageIndex = value < 1 ? DefaultIndex : value;
            }
        }

        private int pageSize;
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize
        {
            get { return this.pageSize; }
            set
            {
                this.pageSize = value < 1 ? DefaultSize : value;
            }
        }
        /// <summary>
        /// 总条数
        /// </summary>
        public long TotalNumber
        {
            get; set;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get; set;
        }
        /// <summary>
        /// 开始
        /// </summary>
        public int Begin
        {
            get => this.PageSize * (this.PageIndex - 1) + 1;
        }
        /// <summary>
        /// 结尾
        /// </summary>
        public int End
        {
            get => this.pageSize * this.PageIndex;
        }
    }
}
