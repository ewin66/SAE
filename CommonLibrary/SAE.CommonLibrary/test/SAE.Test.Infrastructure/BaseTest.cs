using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace SAE.Test.Infrastructure
{
    public abstract class BaseTest
    {
        protected readonly ITestOutputHelper _output;
        public BaseTest(ITestOutputHelper output)
        {
            _output = output;
        }

        protected void WriteLine(object @object)
        {
            this._output.WriteLine(JsonConvert.SerializeObject(@object));
        }
    }
}
