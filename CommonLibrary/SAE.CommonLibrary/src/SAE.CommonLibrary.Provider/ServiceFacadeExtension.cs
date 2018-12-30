using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.Provider
{
    public static class ServiceFacadeExtension
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider)where TService:class
        {
            return serviceProvider.GetService(typeof(TService)) as TService;
        }
    }
}
