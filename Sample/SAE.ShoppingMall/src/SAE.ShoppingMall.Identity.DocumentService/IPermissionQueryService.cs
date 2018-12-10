using SAE.ShoppingMall.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.DocumentService
{
    public interface IPermissionQueryService
    {
        /// <summary>
        /// 根据id获得权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PermissionDto GetById(string id);
    }
}
