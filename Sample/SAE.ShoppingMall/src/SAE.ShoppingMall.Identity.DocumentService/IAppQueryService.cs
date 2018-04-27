using System;
using System.Collections.Generic;
using System.Text;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;
using SAE.ShoppingMall.Infrastructure.Specification;

namespace SAE.ShoppingMall.Identity.DocumentService
{
    public interface IAppQueryService
    {
        /// <summary>
        /// 分页查询应用
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        IPagingResult<AppDto> Paging(IPaging paging, ISpecification<AppDto> specification);
    }
}
