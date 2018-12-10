using SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.EventStore;
using SAE.ShoppingMall.Identity.Domain.Event;
using System;
using System.Collections.Generic;

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
        public ICollection<string> Permissions { get; set; }

        public DateTime CreateTime { get; set; }

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
    }

    public partial class Role
    {
        internal void When(CreateRoleEvent @event)
        {
            this.Id = @event.Id;
            this.CreateTime = @event.CreateTime;
            this.Name = @event.Name;
        }
    }
}
