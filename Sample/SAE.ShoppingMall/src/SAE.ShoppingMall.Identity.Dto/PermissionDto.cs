using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Dto
{
    public class PermissionDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模式
        /// </summary>
        public string Pattern { get; set; }
    }
}
