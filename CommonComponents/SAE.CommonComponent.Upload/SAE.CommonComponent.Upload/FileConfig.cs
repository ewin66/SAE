using SAE.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.Upload
{
    public class FileConfig
    {
        public FileConfig()
        {
            this.directoryFormat = "yyyy-MM";
        }
        public string Host { get; set; }
        public string Path { get; set; }
        private string directoryFormat;
        public string DirectoryFormat
        {
            get=>this.directoryFormat;
            set
            {
                if (value.IsNullOrWhiteSpace()) return;

                this.directoryFormat = value;
            }
        }
        /// <summary>
        /// 格式化目录
        /// </summary>
        /// <returns></returns>
        public string FormattDirectory()
        {
            return DateTime.Now.ToString(this.DirectoryFormat);
        }
    }
}
