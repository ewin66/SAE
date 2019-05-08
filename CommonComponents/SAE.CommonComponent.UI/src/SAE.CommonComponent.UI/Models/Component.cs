using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.UI.Models
{
    public class Component
    {
        public Component()
        {
            this.Childs = new List<Component>();
        }
        public string Id { get => this.Path + this.Name; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public ComponentDescription Description { get; set; }
        public ICollection<Component> Childs { get; set; }
    }
}
