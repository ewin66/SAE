using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.MQ
{
    public class KeyAttribute :Attribute,IKey
    {
        public KeyAttribute(string key)
        {
            this.Key = key;
        }
        public string Key
        {
            get;
        }
    }
}
