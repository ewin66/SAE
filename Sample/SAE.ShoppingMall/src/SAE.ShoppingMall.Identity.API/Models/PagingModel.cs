using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAE.ShoppingMall.Infrastructure;

namespace SAE.ShoppingMall.Identity.API.Models
{
    public class PagingModel : IPaging
    {
        public PagingModel()
        {
            this.Index = 1;
            
        }

        private int index;

        public int Index
        {
            get => this.index;
            set
            {
                if (value <= 0)
                    this.index = 1;
                else
                    this.index = value;
            }
        }

        private int size;
        public int Size
        {
            get => this.size;
            set
            {
                if (value <= 0)
                    this.size = 10;
                else
                    this.size = value;
            }
        }

        public long Count
        {
            get;set;
        }

        public int TotalCount
        {
            get;
        }
    }
}
