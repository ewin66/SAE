using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.Json;
using Xunit.Abstractions;

namespace SAE.ShoppingMall.Test
{
    public abstract class BaseTest
    {
        protected IServiceCollection Services { get; set; }
        protected ITestOutputHelper _testOutputHelper;
        public BaseTest(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            Services = new ServiceCollection();
        }

        protected virtual void Show(object @object)
        {
            this._testOutputHelper.WriteLine(JsonHelper.Serialize(@object));
        }
    }
}
