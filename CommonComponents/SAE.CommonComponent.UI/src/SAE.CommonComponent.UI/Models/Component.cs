using System.Collections.Generic;

namespace SAE.CommonComponent.UI.Models
{
    public class Component
    {
        public Component()
        {
            this.Childs = new List<Component>();
            this.Datas = new List<string>();
        }
        public string Id { get => this.Path + this.Name; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 测试数据集
        /// </summary>
        public IEnumerable<string> Datas { get; set; }
        public ICollection<Component> Childs { get; set; }
    }
}
