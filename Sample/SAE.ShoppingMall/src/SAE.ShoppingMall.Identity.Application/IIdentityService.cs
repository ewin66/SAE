using SAE.ShoppingMall.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Application
{
    public interface IIdentityService
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="credentialsDto"></param>
        UserDto Register(CredentialsDto credentialsDto);
        /// <summary>
        /// 根据id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDto Find(Guid id);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="credentialsDto">账号凭证</param>
        /// <returns></returns>
        UserDto Login(CredentialsDto credentialsDto);
    }
}
