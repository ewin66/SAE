using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.Upload.Code
{
    public class FileContext
    {
        public FileContext(string originalName,Stream stream,HttpContext context)
        {
            this.OriginalName = originalName;
            this.Stream = stream;
            this.HttpContext = context;
        }
        /// <summary>
        /// 原始文件名称
        /// </summary>
        public string OriginalName { get; }
        /// <summary>
        /// 要保存的文件完全名称(d:\xxxx\xx.xx)
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>
        public Stream Stream { get; }
        /// <summary>
        /// 请求上下文
        /// </summary>
        public HttpContext HttpContext { get; }

        public FileResult Result { get; set; }
    }
    public class FileResult
    {
        public string OriginalName { get; set; }
        public string Url => this.Host + FileName;
        public string FileName { get; set; }
        public string Host { get; set; }
    }
}
