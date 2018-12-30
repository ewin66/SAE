using SAE.CommonLibrary.Provider;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ProviderExtension
    {
        public static IServiceProvider UseServiceFacade(this IServiceProvider provider)
        {
            ServiceFacade.Provider = provider;
            return provider;
        }
    }
}
