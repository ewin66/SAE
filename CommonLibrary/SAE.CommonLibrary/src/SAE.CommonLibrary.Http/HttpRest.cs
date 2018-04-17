using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SAE.CommonLibrary.Http
{
    public class HttpRest
    {
        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpRequestMessage Delete(string url)
        {
            return new HttpRequestMessage(HttpMethod.Delete, url);
        }
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpRequestMessage Get(string url)
        {
            return new HttpRequestMessage(HttpMethod.Get, url);
        }
        /// <summary>
        /// Head请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static HttpRequestMessage Head(string url)
        {
            return new HttpRequestMessage(HttpMethod.Head, url);
        }
        /// <summary>
        /// Options请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpRequestMessage Options(string url)
        {
            return new HttpRequestMessage(HttpMethod.Options, url);
        }
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpRequestMessage Post(string url)
        {
            return new HttpRequestMessage(HttpMethod.Post, url);
        }
        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpRequestMessage Put(string url)
        {
            return new HttpRequestMessage(HttpMethod.Put, url);
        }
        /// <summary>
        /// Trace请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpRequestMessage Trace(string url)
        {
            return new HttpRequestMessage(HttpMethod.Trace, url);
        }

    }
}
