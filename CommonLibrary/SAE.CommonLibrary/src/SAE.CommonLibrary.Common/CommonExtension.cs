using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.ObjectMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SAE.CommonLibrary.Common
{
    /// <summary>
    /// 通用函数扩展
    /// </summary>
    public static class CommonExtension
    {
        static CommonExtension()
        {
            extendAction = (s, t) => TinyMapper.Map(t.GetType(), s.GetType(), t, s);
        }
        private static Action<object, object> extendAction;
        /// <summary>
        /// 循环<paramref name="ts"/>集合,并挨个执行<paramref name="action"/>函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> ts, Action<T> action)
        {
            if (ts != null)
            {
                foreach (var itm in ts)
                {
                    action(itm);
                }
            }
            return ts;
        }

        /// <summary>
        /// 设置对象延伸提供
        /// </summary>
        /// <param name="services"></param>
        /// <param name="extendAction"></param>
        public static void SettingExtendProvider(IServiceCollection services, Action<object, object> extendAction)
        {
            CommonExtension.extendAction = extendAction;
        }

        /// <summary>
        /// 将<paramref name="attach"/>附加到<paramref name="source"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">对象源</param>
        /// <param name="attach">要附加的对象</param>
        public static void Extend<T>(this T source, object attach) where T : class
        {
            Assert.Build(source)
                  .NotNull();
            Assert.Build(attach)
                  .NotNull();
            extendAction.Invoke(source, attach);
        }

        /// <summary>
        /// 将对象<paramref name="input"/>转换成<typeparamref name="TEnum"/>
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TEnum EnumTo<TEnum>(this string input) where TEnum : struct
        => EnumTo<TEnum>(input);


        private static TEnum EnumTo<TEnum>(object o) where TEnum : struct
        {
            TEnum @enum;
            Enum.TryParse(o.ToString(), out @enum);
            return @enum;
        }

        /// <summary>
        /// 将对象<paramref name="input"/>转换成<typeparamref name="TEnum"/>
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TEnum EnumTo<TEnum>(this int input) where TEnum : struct
        => EnumTo<TEnum>(input);

        /// <summary>
        /// 获得<paramref name="enum"/>Display“Name”
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string Display(this Enum @enum)
        {
            var name = @enum.ToString();
            var field = @enum.GetType().GetField(name);
            if (field == null) return name;
            var attribute = field.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault();
            return attribute == null ? field.Name : attribute.GetName();
        }

        /// <summary>
        /// 映射对象
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static TModel To<TModel>(this object o)
        {
            if (o == null) return default(TModel);

            return TinyMapper.Map<TModel>(o);
        }

        /// <summary>
        /// 如果<paramref name="str"/>为null或空字符串或一连串空的字符串则返回<seealso cref="bool.TrueString"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
