using SAE.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.Upload
{
    public class FileConfig
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public string DirectoryFormat { get; set; }
        public Tuple<string,string> GetPath(string fileName)
        {
            var date = DateTime.Now.ToString(this.DirectoryFormat ?? "yyyy-MM");

            var guid = Utils.GenerateId();
            
            var dir = System.IO.Path.Combine(this.Path, date);

            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            var file = $"{guid}{System.IO.Path.GetExtension(fileName)}";

            return new Tuple<string, string>(System.IO.Path.Combine(dir, file), System.IO.Path.Combine(date, file).Replace("\\","/"));
        }
    }
}
