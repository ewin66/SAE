using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.ConfigServer.Models
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int SolutionId { get; set; }
        public DateTime CreateTime { get; set; }
        public int Version { get; set; }
    }
}
