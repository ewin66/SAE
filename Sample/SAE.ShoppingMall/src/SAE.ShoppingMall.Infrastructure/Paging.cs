using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Infrastructure
{
    public class Paging : IPaging
    {
        protected const int DefaultIndex = 1;
        protected const int DefaultSize = 10;
        public Paging()
        {
            this.pageIndex = DefaultIndex;
            this.PageSize = DefaultSize;
        }
        private int pageIndex;
        public int PageIndex
        {
            get { return this.pageIndex; }
            set
            {
                this.pageIndex = value < 1 ? DefaultIndex : value;
            }
        }

        private int pageSize;
        public int PageSize
        {
            get { return this.pageSize; }
            set
            {
                this.pageSize = value < 1 ? DefaultSize : value;
            }
        }

        public long TotalNumber
        {
            get; set;
        }

        public int PageCount
        {
            get; set;
        }

        public int Begin
        {
            get => this.PageSize * (this.PageIndex - 1) + 1;
        }
        public int End
        {
            get => this.pageSize * this.PageIndex;
        }
    }
}
