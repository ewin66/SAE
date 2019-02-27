using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.Upload.Code
{
    public class UploadDecorator : FileDecorator
    {
        public override async Task Decorate(FileContext context)
        {
            if (context.Stream.Position > 0)
            {
                context.Stream.Position = 0;
            }
            using (var fileStream = new FileStream(context.FileName, FileMode.Create))
            {
                await context.Stream.CopyToAsync(fileStream);
            }
            await base.Decorate(context);
        }
    }
}
