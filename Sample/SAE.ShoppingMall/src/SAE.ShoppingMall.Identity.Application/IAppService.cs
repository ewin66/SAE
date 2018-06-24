using System;
using System.Collections.Generic;
using System.Text;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;

namespace SAE.ShoppingMall.Identity.Application
{
    /// <summary>
    /// 应用服务
    /// </summary>
    public interface IAppService
    {
        /// <summary>
        /// 注册应用
        /// </summary>
        /// <param name="appDto"></param>
        /// <returns></returns>
        AppDto Register(AppDto appDto);
        /// <summary>
        /// 更改应用
        /// </summary>
        /// <param name="appDto"></param>
        void Change(AppDto appDto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="appId"></param>
        void Remove(string appId);

        /// <summary>
        /// 根据appid获得对象
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        AppDto Find(string appId);
    }
}
