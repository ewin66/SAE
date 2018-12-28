using SAE.CommonLibrary.ObjectMapper.Bindings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.ObjectMapper
{
   
    public static class TinyMapperExension
    {
        /// <summary>
        /// 反转绑定
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="binding"></param>
        /// <param name="config"></param>
        public static void Reverse<TSource, TTarget>(this IReverseBinding<TSource, TTarget> binding, Action<IBindingConfig<TSource, TTarget>> config = null)
        {
            TinyMapper.Bind<TSource, TTarget>(bind =>
            {
                foreach (var kv in binding.BindExpression)
                {
                    bind.Bind(kv.Value, kv.Key);
                }
                config?.Invoke(bind);
            });
        }

    }
}
