using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore
{
    public class Identity:IIdentity
    {
        public string Id { get;  }
        public Identity()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public Identity(string id)
        {
            this.Id = id;
        }
        public override string ToString()
        {
            return this.Id;
        }
    }
}
