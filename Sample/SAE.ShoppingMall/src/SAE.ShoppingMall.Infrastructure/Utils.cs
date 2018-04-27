using System;
using System.Collections.Generic;
using System.Text;
using Nelibur.ObjectMapper;

namespace SAE.ShoppingMall.Infrastructure
{
    public class Utils
    {
        public static TModel Map<TModel>(object o) => TinyMapper.Map<TModel>(o);
        public static TEnum EnumTo<TEnum>(object o) where TEnum : struct
        {
            TEnum @enum;
            Enum.TryParse<TEnum>(o.ToString(), out @enum);
            return @enum;
        }
    }
}
