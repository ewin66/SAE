using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Dto
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
