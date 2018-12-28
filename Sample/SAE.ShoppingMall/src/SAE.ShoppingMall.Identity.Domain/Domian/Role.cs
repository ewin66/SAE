using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.EventStore;
using SAE.ShoppingMall.Identity.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SAE.ShoppingMall.Identity.Domain
{
    public partial class Role: AggregateRoot
    {
        public Role()
        {
            this.Permissions = new List<string>();
        }

        public Role(string name)
        {
            this.Create(name);
        }
        public override IIdentity Identity => IdentityGenerator.Build(this.Id);
        public string Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限集合
        /// </summary>
        public IEnumerable<string> Permissions { get; set; }

        public DateTime CreateTime { get; set; }

        public void Clear()
        {
            this.Apply(new GrantRolePermissionEvent { Permissions = Enumerable.Empty<string>() });
        }

        public void AddRange(IEnumerable<string> permissions)
        {
            this.Apply(new GrantRolePermissionEvent { Permissions = permissions });
        }
    }

    public partial class Role
    {
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="flag">请求输入</param>
        /// <param name="permissionProvider">权限提供者</param>
        /// <returns></returns>

        public bool Authorize(string flag, Func<IIdentity, Permission> permissionProvider)
        {
            foreach (CommonLibrary.EventStore.Identity permissionId in this.Permissions)
            {
                var permission = permissionProvider.Invoke(permissionId);

                if (permission.Comparison(flag))
                    return true;
            }
            return false;
        }

        public void Create(string name)
        {
            Assert.Build(name,"角色名称")
                  .NotNullOrWhiteSpace(name);

            this.Apply(new CreateRoleEvent
            {
                Name = name,
                Id = IdentityGenerator.Build().ToString(),
                CreateTime = DateTime.Now
            });
        }

        public void AddRange(IEnumerable<Permission> permissions)
        {
            if (permissions.Any())
            {
                this.Apply(new GrantRolePermissionEvent
                {
                    Permissions = permissions.Select(s => s.Id)
                });
            }
        }
        public void Change(Role role)
        {
            if (this.Name != role.Name) { }
            this.Apply(new ChangeRoleEvent
            {
                Name = role.Name
            });
        }
        
    }
}
