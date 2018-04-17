
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SAE.CommonLibrary.Json
{
    /// <summary>
    /// Json序列化和反序列化类库
    /// </summary>
    public sealed class JsonHelper
    {
        private static readonly IJsonConvertor _default = new Imp.JsonConvertor();
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string Serialize(object o)
        {
            return _default.Serialize(o);
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串
        /// <remarks>示例：{"ID":"112","Name":"石子儿"}</remarks>
        /// </param>
        /// <returns>对象实体</returns>
        public static T Deserialize<T>(string json)
        {
            return _default.Deserialize<T>(json);
        }
        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="type">要被反序列化的类型</param>
        /// <returns></returns>
        public static object Deserialize(string json,Type type)
        {
            return _default.Deserialize(json,type);
        }

    }
}