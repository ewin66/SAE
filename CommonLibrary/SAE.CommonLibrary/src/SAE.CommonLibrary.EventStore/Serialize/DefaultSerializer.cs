using SAE.CommonLibrary.Json;
using System;

namespace SAE.CommonLibrary.EventStore.Serialize
{
    public class DefaultSerializer : ISerializer
    {
        public DefaultSerializer()
        {
            
        }

        public object Deserialize(string input, Type type)
        {
            return JsonHelper.Deserialize(input, type);
        }

        public string Serialize(object @object)
        {
            return JsonHelper.Serialize(@object);
        }
    }
}
