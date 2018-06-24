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
            return Build<TResult>(paging.Index, paging.Size, paging.Count, results);
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
            
            this.results = new List<TResult>(lists ?? new List<TResult>());

            this.Count = count;

            if (this.Count > 0)
            {
                this.Size = size <= 0 ? 10 : size;
                this.Count = count;
                this.Total = (int)Math.Ceiling(this.Total / (this.Size * 1.0M));
                this.Index = this.Index > this.Total ? this.Total : this.Index;
            }
            
        }

        private readonly IEnumerable<TResult> results;
        [JsonProperty("index")]
        public int Index
        {
            get;
        }
        [JsonProperty("size")]
        public int Size { get; }
        [JsonProperty("count")]
        public long Count { get; set; }
        [JsonProperty("total")]
        public int Total { get; }
        [JsonProperty("items")]
        public IEnumerable<TResult> Items { get => this.results; }
        public IEnumerator<TResult> GetEnumerator() => this.results.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
