using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.Upload.Code
{
    /// <summary>
    /// 文件装饰器
    /// </summary>
    public interface IFileDecorator
    {
        /// <summary>
        /// 装饰对象
        /// </summary>
        /// <param name="context">文件上下文</param>
        Task Decorate(FileContext context);
    }

    public abstract class FileDecorator : IFileDecorator
    {
        private FileDecorator decorator;
        public FileDecorator()
        {

        }

        public virtual void SetDecorator(FileDecorator decorator)
        {
            this.decorator = decorator;
        }

        public virtual async Task Decorate(FileContext context)
        {
            if (this.decorator != null)
            {
                await this.decorator.Decorate(context);
            }
            
        }
    }
}
