using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SAE.CommonLibrary.Json.Imp
{
    /// <summary>
    /// 默认实现，使用前请通过Nuget包下载Json.Net(包Id：Newtonsoft.Json)
    /// </summary>
    public class JsonConvertor : IJsonConvertor
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public JsonConvertor()
        {
            _lowerCaseSetting = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
        /// <summary>
        /// 小写设置
        /// </summary>
        private readonly JsonSerializerSettings _lowerCaseSetting;
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public string Serialize(object @object)
        {

            return JsonConvert.SerializeObject(@object,this._lowerCaseSetting);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
    }
}