using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.Infrastructure;
using SAE.ShoppingMall.Infrastructure.Specification;

namespace SAE.ShoppingMall.DocumentService
{
    /// <summary>
    /// 文档服务基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public abstract class DocumentService<TDto> : IDocumentService<TDto> where TDto : class
    {
        protected IStorage Storage
        {
            get;
        }
        public DocumentService(IStorage storage)
        {
            this.Storage = storage;
        }
        public virtual void Add(TDto dto)
        {
            this.Storage.Add(dto);
        }

        public void Update(TDto dto)
        {
            this.Storage.Update(dto);
        }

        public virtual IPagingResult<TDto> Paging(IPaging paging, ISpecification<TDto> specification)
        {
            var query = this.Storage.AsQueryable<TDto>();
            if (specification?.Expression != null)
            {
                query = query.Where(specification.Expression);
            }

            paging.Count = query.LongCount();

            return PagingResult.Build(paging, query.Skip((paging.Index - 1) * paging.Size).Take(paging.Size));
        }

    
    }
}
