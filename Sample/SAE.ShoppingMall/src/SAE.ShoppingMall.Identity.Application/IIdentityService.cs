using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Identity.Dto.Query;
using SAE.ShoppingMall.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Application
{
    public interface IIdentityService
    {
        #region User
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="credentialsDto"></param>
        void Create(CredentialsDto credentialsDto);
        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="id"></param>
        void RemoveUser(string id);
        /// <summary>
        /// 更改用户属性
        /// </summary>
        /// <param name="dto"></param>
        void Update(UserDto dto);

        /// <summary>
        /// 根据id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDto GetById(string id);
        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="credentialsDto">账号凭证</param>
        /// <returns></returns>
        UserDto Authentication(CredentialsDto credentialsDto);
        /// <summary>
        /// 根据用户获得账号
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        UserDto GetByLoginName(string loginName);

        /// <summary>
        /// 验证<paramref name="userId"/>用户是否有<paramref name="flag"/>权限
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="flag">权限标记</param>
        /// <returns>true拥有权限，false不拥有</returns>
        bool Authorization(string userId, string flag);
        /// <summary>
        /// 授予用户角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="roles">角色列表</param>
        void GrantUserRoles(string userId, IEnumerable<string> roles);
        /// <summary>
        /// 分页获得用户
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IPagingResult<UserDto> Paging(UserQuery query);
        #endregion

        #region Role
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="dto"></param>
        void Create(RoleDto dto);
        void Update(RoleDto dto);
        void RemoveRole(string roleId);
        /// <summary>
        /// 授予角色权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="permissions">权限列表</param>
        void GrantRolePermissions(string roleId, IEnumerable<string> permissions);
        #endregion

        #region Permission

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="dto"></param>
        void Create(PermissionDto dto);
        void Update(PermissionDto dto);
        void RemovePermission(string permissionId);

        #endregion

    }
}
