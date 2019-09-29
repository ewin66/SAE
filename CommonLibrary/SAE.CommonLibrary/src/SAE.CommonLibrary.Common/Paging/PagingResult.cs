using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SAE.CommonLibrary.Common
{
    /// <summary>
    /// 分页结果
    /// </summary>
    public class PagingResult
    {
        /// <summary>
        /// 构建一个分页结果
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="paging"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IPagingResult<TResult> Build<TResult>(IPaging paging,IEnumerable<TResult> results)
        {
            return Build(paging.PageIndex, paging.PageSize, paging.TotalNumber, results);
        }

        /// <summary>
        /// 构建一个分页结果
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IPagingResult<TResult> Build<TResult>(int index, int size, long count, IEnumerable<TResult> results)
        {
            return new PagingResult<TResult>(index, size, count, results);
        }
    }
    
    /// <summary>
    /// 分页结果实现
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [JsonObject]
    public class PagingResult<TResult> : IPagingResult<TResult>
    {
        internal PagingResult(int index,int size,long count,IEnumerable<TResult> lists)
        {
            this.Items = new List<TResult>(lists ?? new List<TResult>());
            this.PageIndex = index;
            this.PageSize = size <= 0 ? 10 : size;
            this.TotalNumber = count;
            
            if (this.TotalNumber > 0)
            {
                this.TotalNumber = count;
                this.PageCount = (int)Math.Ceiling(this.TotalNumber / (this.PageSize * 1.0M));
                this.PageIndex = this.PageIndex > this.PageCount ? this.PageCount : this.PageIndex;
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pageIndex")]
        public int PageIndex
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("totalNumber")]
        public long TotalNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pageCount")]
        public int PageCount { get; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("items")]
        public IEnumerable<TResult> Items { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TResult> GetEnumerator() => this.Items.GetEnumerator();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
