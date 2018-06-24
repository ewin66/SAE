using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.DocumentService
{
    public interface IDocumentService<TDto>where TDto:class
    {
        /// <summary>
        /// 添加传输对象
        /// </summary>
        /// <param name="dto"></param>
        void Add(TDto dto);
        /// <summary>
        /// 更新传输对象
        /// </summary>
        /// <param name="dto"></param>
        void Update(TDto dto);
    }
}
