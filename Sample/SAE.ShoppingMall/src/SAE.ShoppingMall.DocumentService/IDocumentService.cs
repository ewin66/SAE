using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.DocumentService
{
    public interface IDocumentService<TDto>where TDto:class
    {
        /// <summary>
        /// 保存传输对象
        /// </summary>
        /// <param name="dto"></param>
        void Save(TDto dto);
    }
}
