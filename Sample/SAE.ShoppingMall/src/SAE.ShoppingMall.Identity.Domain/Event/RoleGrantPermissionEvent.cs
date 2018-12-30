using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    /// <summary>
    /// 授予角色权限事件
    /// </summary>
    public class RoleGrantPermissionEvent:Event
    {
        public IEnumerable<string> Permissions { get; set; }
    }
}
