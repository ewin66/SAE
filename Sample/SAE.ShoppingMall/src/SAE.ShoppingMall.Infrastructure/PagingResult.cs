using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SAE.ShoppingMall.Infrastructure
{
    public class PagingResult
    {
        public static IPagingResult<TResult> Build<TResult>(IPaging paging,IEnumerable<TResult> results)
        {
            return Build(paging.PageIndex, paging.PageSize, paging.TotalNumber, results);
        }

        public static IPagingResult<TResult> Build<TResult>(int index, int size, long count, IEnumerable<TResult> results)
        {
            return new PagingResult<TResult>(index, size, count, results);
        }
    }
    
    [JsonObject]
    public class PagingResult<TResult> : IPagingResult<TResult>
    {
        internal PagingResult(int index,int size,long count,IEnumerable<TResult> lists)
        {
            
            this.Items = new List<TResult>(lists ?? new List<TResult>());

            this.TotalNumber = count;

            if (this.TotalNumber > 0)
            {
                this.PageSize = size <= 0 ? 10 : size;
                this.TotalNumber = count;
                this.PageCount = (int)Math.Ceiling(this.PageCount / (this.PageSize * 1.0M));
                this.PageIndex = this.PageIndex > this.PageCount ? this.PageCount : this.PageIndex;
            }
            
        }

        [JsonProperty("pageIndex")]
        public int PageIndex
        {
            get;
        }
        [JsonProperty("pageSize")]
        public int PageSize { get; }
        [JsonProperty("totalNumber")]
        public long TotalNumber { get; set; }
        [JsonProperty("pageCount")]
        public int PageCount { get; }
        [JsonProperty("items")]
        public IEnumerable<TResult> Items { get; }
        public IEnumerator<TResult> GetEnumerator() => this.Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
