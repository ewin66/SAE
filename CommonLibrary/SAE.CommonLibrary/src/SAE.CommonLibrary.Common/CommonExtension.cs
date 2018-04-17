using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAE.CommonLibrary.Common
{
    public static class CommonExtension
    {
        /// <summary>
        /// 循环<paramref name="ts"/>集合,并挨个执行<paramref name="action"/>函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> ts,Action<T> action)
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
    }
}
