using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Builder
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =true,Inherited =false)]
    public sealed class ModelAttribute:Attribute
    {
        internal Type Type { get;}

        public ModelAttribute(string typeName)
        {
            var type= Type.GetType(typeName);
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentNullException(nameof(typeName));

            if (type == null)
                throw new ArgumentException($"{typeName}不存在");

            this.Type = type;
        }
    }
}
