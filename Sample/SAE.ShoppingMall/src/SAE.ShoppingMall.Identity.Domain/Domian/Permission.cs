using System;
using System.Text.RegularExpressions;
using SAE.CommonLibrary.EventStore;
using SAE.ShoppingMall.Identity.Domain.Event;

namespace SAE.ShoppingMall.Identity.Domain
{
    public partial class Permission: AggregateRoot
    {
        public Permission()
        {
            
        }

        public Permission(string name)
        {
            this.Name = name;
            this.Id = IdentityGenerator.Build().ToString();
        }
        public override IIdentity Identity => this.Id.ToIdentity();
        public string Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模式
        /// </summary>
        public string Pattern { get; set; }

        public string Group { get; set; }

        /// <summary>
        /// 比对两者是否匹配
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Comparison(string input)
        {
            return Regex.Match(input, this.Pattern).Success;
        }
    }

    public partial class Permission
    {
        public void Create(Permission permission)
        {
            this.Apply(new CreatePermissionEvent
            {
                Name = permission.Name,
                Group = permission.Group,
                Id = permission.Id,
                Pattern = permission.Pattern
            });
        }

        public void Change(Permission permission)
        {
            this.Apply(new ChangePermissionEvent
            {
                Name = permission.Name,
                Group = permission.Group,
                Pattern = permission.Pattern
            });
        }
    }

    public partial class Permission
    {
        internal void When(ChangePermissionEvent @event)
        {
            
        }

        internal void When(CreatePermissionEvent @event)
        {
        }
    }
}
