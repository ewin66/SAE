using SAE.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.Upload.Code
{
    public class NamingDecorator : FileDecorator
    {
        private readonly FileConfig _fileConfig;
        public NamingDecorator(FileConfig fileConfig)
        {
            this._fileConfig = fileConfig;
        }

        public override async Task Decorate(FileContext context)
        {

            var date = this._fileConfig.FormattDirectory(); ;

            var guid = Utils.GenerateId();

            var dir = Path.Combine(this._fileConfig.Path, date);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var file = $"{guid}{Path.GetExtension(context.OriginalName)}";

            context.FileName = Path.Combine(dir, file);

            context.Result = new FileResult
            {
                FileName = file,
                Host = this._fileConfig.Host,
                OriginalName = context.OriginalName,
            };
            await base.Decorate(context);
        }
    }
}
