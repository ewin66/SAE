using System;
using System.Collections.Generic;

namespace SAE.CommonComponent.UI.Models
{
    public class Library
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public IEnumerable<string> Dependencies { get; set; }

        public void Unique(Func<string, Library> search)
        {
            if (search.Invoke(this.Name) != null)
                throw new Exception($"'{this.Name}'已存在");
        }
    }
}
