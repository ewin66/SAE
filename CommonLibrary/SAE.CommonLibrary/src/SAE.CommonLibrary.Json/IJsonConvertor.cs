using System;

namespace SAE.CommonLibrary.Json
{
    /// <summary>
    /// Json转换器接口
    /// </summary>
    public interface IJsonConvertor
    {
        /// <summary>
        /// 序列化成Json
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        string Serialize(object @object);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        object Deserialize(string json);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        object Deserialize(string json, Type type);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        T Deserialize<T>(string json);
    }
}