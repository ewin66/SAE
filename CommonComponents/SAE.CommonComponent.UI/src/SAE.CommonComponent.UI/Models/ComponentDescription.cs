using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.UI.Models
{
    public class ComponentDescription
    {
        /// <summary>
        /// 属性
        /// </summary>
        public IDictionary<string,string> Props { get; set; }
        /// <summary>
        /// 测试数据
        /// </summary>
        public string Data { get; set; }
    }
}
