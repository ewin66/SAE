using SAE.CommonLibrary.EventStore;
using System;
using System.Collections.Generic;

namespace SAE.ShoppingMall.Identity.Domain
{
    public class Role: AggregateRoot
    {
        public Role()
        {
            this.Permissions = new List<string>();
        }
        public override IIdentity Identity => throw new NotImplementedException();
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限集合
        /// </summary>
        public ICollection<string> Permissions { get; set; }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="input">请求输入</param>
        /// <param name="permissionProvider">权限提供者</param>
        /// <returns></returns>

        public bool Authorize(string input, Func<IIdentity, Permission> permissionProvider)
        {
            foreach (string permissionId in this.Permissions)
            {
                var id = IdentityGenerator.Build(permissionId);

                var permission = permissionProvider.Invoke(id);

                if (permission.Comparison(input))
                    return true;
            }

            return false;
        }
    }
}
