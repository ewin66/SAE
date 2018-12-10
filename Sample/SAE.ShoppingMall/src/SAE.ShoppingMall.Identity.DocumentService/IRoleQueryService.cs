using SAE.ShoppingMall.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.DocumentService
{
    public interface IRoleQueryService
    {
        /// <summary>
        /// 根据id获得角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleDto GetById(string id);
        /// <summary>
        /// 根据<paramref name="Name"/>获得角色
        /// </summary>
        /// <param name="Name">角色名称</param>
        /// <returns></returns>
        RoleDto GetByName(string Name);

    }
}
