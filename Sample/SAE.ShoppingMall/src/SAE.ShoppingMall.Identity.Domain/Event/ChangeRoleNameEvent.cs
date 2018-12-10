using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class ChangeRoleNameEvent:Event
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
    }
}
