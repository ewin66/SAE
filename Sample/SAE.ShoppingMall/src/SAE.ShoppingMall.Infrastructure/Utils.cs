using System;
using System.Collections.Generic;
using System.Text;
using Nelibur.ObjectMapper;

namespace SAE.ShoppingMall.Infrastructure
{
    public class Utils
    {
        public static TModel Map<TModel>(object o) => TinyMapper.Map<TModel>(o);
    }
}
