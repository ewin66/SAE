using SAE.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.Upload
{
    public class FileDescription
    {
        private readonly Stream _stream;
        private readonly FileConfig _config;
        public FileDescription()
        {
                
        }
        public FileDescription(FileConfig fileConfig,string originName,Stream stream)
        {
            this.OriginName = originName;
            this.Host = fileConfig.Host;
            this._config = fileConfig;
            this._stream = stream;
        }

        public async Task SaveAsync()
        {
            var fileTuple = this._config.GetPath(this.OriginName);
            using (var fileStream = new FileStream(fileTuple.Item1, FileMode.Create))
            {
                await _stream.CopyToAsync(fileStream);
                this.FileName = fileTuple.Item2;
            }
        }

        /// <summary>
        /// 源名称
        /// </summary>
        public string OriginName { get;}
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url => this.Host + FileName;
        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; set; }
    }
}
