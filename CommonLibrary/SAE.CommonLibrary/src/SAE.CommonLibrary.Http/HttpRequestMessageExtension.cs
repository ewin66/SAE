using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.Http
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpRequestMessageExtension
    {
        internal static readonly Lazy<IJsonConvertor> _jsonConvertor = new Lazy<IJsonConvertor>(ServiceFacade.Provider.GetService<IJsonConvertor>);
        /// <summary>
        /// 
        /// </summary>
        public static HttpClient Client;
        static HttpRequestMessageExtension()
        {
            Client = new HttpClient(new HttpClientHandler { UseProxy = false });
        }
        /// <summary>
        /// 默认的字符集
        /// </summary>
        public static Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// 设置<seealso cref="HttpClient"/>
        /// </summary>
        /// <param name="httpClient"></param>
        public static void SetHttpClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        /// <summary>
        /// 添加请求头
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HttpRequestMessage AddHeader(this HttpRequestMessage request,string key,string value)
        {
            return request.AddHeader(key,new string[] { value });
        }
        /// <summary>
        /// 添加请求头
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static HttpRequestMessage AddHeader(this HttpRequestMessage request, string key,params string[] values)
        {
            request.Headers.TryAddWithoutValidation(key, values);
            return request;
        }

        /// <summary>
        /// 以字符串的形式添加数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static HttpRequestMessage AddContent(this HttpRequestMessage request,
                                                    string value,
                                                    string mediaType="application/json")
        {
            if (string.IsNullOrWhiteSpace(value)) return request;

            var content = new StringContent(value, DefaultEncoding, mediaType);
   
            return request.AddContent(content: content);
        }

        /// <summary>
        /// 以键值对的形式添加数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="nameValue"></param>
        /// <returns></returns>
        public static HttpRequestMessage AddContent(this HttpRequestMessage request,IDictionary<string,string> nameValue)
        {
            var content = new FormUrlEncodedContent(nameValue);
            
            foreach(var nv in nameValue)
            {
                var stringContent = new StringContent(nv.Value, Encoding.UTF8);
                stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = nv.Key
                };
                request.AddContent(content: stringContent);
            }

            return request;
        }

        /// <summary>
        /// 以Json形式添加数据
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="request"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static HttpRequestMessage AddJsonContent<TModel>(this HttpRequestMessage request, TModel model) where TModel : class
        {
            var json = model == null ? "" : _jsonConvertor.Value
                                                         .Serialize(model);
            
            return request.AddContent(value: json);
        }
        /// <summary>
        /// 添加流Content
        /// </summary>
        /// <param name="request"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static HttpRequestMessage AddContent(this HttpRequestMessage request, Stream stream)
        {
            return request.AddContent(content: new StreamContent(stream));
        }

        /// <summary>
        /// 添加文件Content
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fileInfo"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpRequestMessage AddContent(this HttpRequestMessage request, FileInfo fileInfo,string name=null)
        {
            if (string.IsNullOrWhiteSpace(name)) name = Guid.NewGuid().ToString();

            var stream = new StreamContent(fileInfo.OpenRead());

            var content = new MultipartFormDataContent();

            content.Add(stream, name, fileInfo.Name);

            return request.AddContent(content: content);
        }

        /// <summary>
        /// 添加HttpContent
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static HttpRequestMessage AddContent(this HttpRequestMessage request,HttpContent content)
        {
            
            if(request.Content != null)
            {
                //当前内容不为null
                MultipartContent multipart;
                if (request.Content is MultipartContent)
                {
                    //当前内容为多内容实例
                    multipart = request.Content as MultipartContent;
                }
                else
                {
                    //初始化一个多内容的实例
                    multipart = new MultipartFormDataContent();
                    //将当前请求内容附加至多内容
                    multipart.Add(request.Content);
                    //多内容附加至内容
                    request.Content = multipart;
                }
                
                if(content is MultipartContent)
                {
                    //当前传输内容为多内容格式，将其挨个附加至请求从内容中
                    foreach(var ct in content as MultipartContent)
                    {
                        multipart.Add(ct);
                    }
                }
                else
                {
                    //但内容附加
                    multipart.Add(content);
                }
               
            }
            else
            {
                //单内容
                request.Content = content;
            }

            return request;
        }
        /// <summary>
        /// 添加请求属性集
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Properties"></param>
        /// <returns></returns>
        public static HttpRequestMessage AddProperty(this HttpRequestMessage request,IDictionary<string,object> Properties)
        {
            foreach (var p in Properties)
                request.Properties[p.Key] = p.Value;
            return request;
        }

        /// <summary>
        /// 发送请求,并返回响应 async
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> SendAsync(this HttpRequestMessage request)
        {
            return Client.SendAsync(request);
        }
        /// <summary>
        /// 发送请求,并返回响应
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HttpResponseMessage Send(this HttpRequestMessage request)
        {
            var task = request.SendAsync();
            task.Wait();
            return  task.Result;
        }

        
    }
}
