using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Infrastructure
{
    public class PagingResult
    {
        public static IPagingResult<TResult> Build<TResult>(IPaging paging,IEnumerable<TResult> results)
        {
            return Build<TResult>(paging.Index, paging.Size, paging.Count, results);
        }

        public static IPagingResult<TResult> Build<TResult>(int index, int size, long count, IEnumerable<TResult> results)
        {
            return new PagingResult<TResult>(index, size, count, results);
        }
    }


    public class PagingResult<TResult> : IPagingResult<TResult>
    {
        internal PagingResult(int index,int size,long count,IEnumerable<TResult> lists)
        {
            this.results = new List<TResult>(lists ?? new List<TResult>());

            this.Count = count;

            if (this.Count > 0)
            {
                this.Size = size <= 0 ? 10 : size;
                this.Count = count;
                this.TotalCount = (int)Math.Ceiling(this.TotalCount / (this.Size * 1.0M));
                this.Index = this.Index > this.TotalCount ? this.TotalCount : this.Index;
            }
            
        }

        private readonly IEnumerable<TResult> results;

        public int Index
        {
            get;
        }

        public int Size { get; }

        public long Count { get; set; }
        public int TotalCount { get; }

        public IEnumerator<TResult> GetEnumerator() => this.results.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
