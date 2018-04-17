using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Collections.Concurrent;

namespace SAE.CommonLibrary.MQ
{
    /// <summary>
    /// 名称工具类
    /// </summary>
    public static class NameUtils
    {
        private static readonly ConcurrentDictionary<Type, string> _dic = new ConcurrentDictionary<Type, string>();
        /// <summary>
        /// 添加映射
        /// </summary>
        internal static void Add(Type type,string key)
        {
            _dic[type] = key;
        }
        /// <summary>
        /// 获得队列名称
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <returns></returns>
        public static string Get<TMessage>()
        {
            return Get(typeof(TMessage));
        }
        /// <summary>
        /// 获得队列名称，此函数将会以<paramref name="message"/>的真实类型来作参考
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string Get(object message)
        {
            return Get(message.GetType());
        }

        /// <summary>
        /// 获得队列名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Get(Type type)
        {
            if (!_dic.ContainsKey(type))
            {
                Add(type);
            }
            return _dic[type];
        }

        /// <summary>
        /// 添加映射
        /// </summary>
        /// <param name="type"></param>
        private static void Add(Type type)
        {
            string key = string.Empty;
            
            var attr = type.GetTypeInfo()
                           .GetCustomAttributes()
                           .OfType<IKey>()
                           .FirstOrDefault();

            if (attr == null)
            {
                key= $"{type.Namespace}.{type.Name}".Replace(".","_");
            }
            else
            {
                key = attr.Key;
            }
            _dic.AddOrUpdate(type, key, (t, k) => k);
        }
    }
}
