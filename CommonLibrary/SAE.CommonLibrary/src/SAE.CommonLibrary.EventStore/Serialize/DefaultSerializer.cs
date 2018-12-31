using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.Provider;
using System;

namespace SAE.CommonLibrary.EventStore.Serialize
{
    public class DefaultSerializer : ISerializer
    {
        private readonly Lazy<IJsonConvertor> _jsonConvertor;
        public DefaultSerializer()
        {
            _jsonConvertor = new Lazy<IJsonConvertor>(ServiceFacade.Provider.GetService<IJsonConvertor>);
        }

        public object Deserialize(string input, Type type)
        {
            return _jsonConvertor.Value.Deserialize(input, type);
        }

        public string Serialize(object @object)
        {
            return _jsonConvertor.Value.Serialize(@object);
        }
    }
}
