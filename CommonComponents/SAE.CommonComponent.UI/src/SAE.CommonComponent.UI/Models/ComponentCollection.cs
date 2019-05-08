using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.UI.Models
{
    public class ComponentCollection
    {
        public ComponentCollection()
        {
            this.Childs = new List<ComponentCollection>();
        }

        public string Type { get; set; }
        public IEnumerable<Component> Components { get; set; }
        public IEnumerable<ComponentCollection> Childs { get; set; }
    }
}
