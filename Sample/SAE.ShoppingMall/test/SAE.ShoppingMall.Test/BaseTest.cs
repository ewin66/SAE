using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace SAE.ShoppingMall.Test
{
    public abstract class BaseTest
    {
        protected readonly IServiceCollection _services;
        protected ITestOutputHelper _testOutputHelper;
        public BaseTest(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            _services = new ServiceCollection();
        }

        protected virtual void Show(object @object)
        {
            this._testOutputHelper.WriteLine(CommonLibrary.Json.JsonHelper.Serialize(@object));
        }
    }
}
