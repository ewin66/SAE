using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.DocumentSearch
{
    public interface IDocumentSearch
    {
        void Add(IDocment document);
        void Remove(IDocment document);
        void Update(IDocment document);
        void Save(IDocment document);
        /// <summary>
        /// 检索文件
        /// </summary>
        /// <param name="id"></param>
        TDocument Get<TDocument>(string id);
    }
}
