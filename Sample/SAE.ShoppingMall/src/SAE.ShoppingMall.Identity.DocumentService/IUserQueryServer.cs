using System.Collections.Generic;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;
using SAE.ShoppingMall.Infrastructure.Specification;

namespace SAE.ShoppingMall.Identity.DocumentService
{
    public interface IUserQueryServer
    {
        /// <summary>
        /// 使用用户名查询用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        UserDto Find(string loginName);
        /// <summary>
        /// 分页查询用户
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        IPagingResult<UserDto> Paging(IPaging paging,ISpecification<UserDto> specification);
    }
}
