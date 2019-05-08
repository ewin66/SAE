using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.UI.Models
{
    /// <summary>
    /// 软件包
    /// </summary>
    public class Package
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public string Path { get; set; }
        /// <summary>
        /// 依赖包
        /// </summary>
        public Package[] Dependencys { get; set; }
    }
}
