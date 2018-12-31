using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
using Xunit.Abstractions;

namespace SAE.ShoppingMall.Test
{
    public abstract class BaseTest
    {
        protected readonly ITestOutputHelper _testOutputHelper;
        protected readonly IServiceCollection _services;
        public BaseTest(ITestOutputHelper testOutputHelper)
        {
            this._services = new ServiceCollection();
            this._testOutputHelper = testOutputHelper;
        }

        protected virtual void Show(object @object)
        {
            this._testOutputHelper.WriteLine(JsonConvert.SerializeObject(@object));
        }

        /// <summary>
        /// 随机数
        /// </summary>
        /// <returns></returns>
        protected string Random()
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                string text = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(Guid.NewGuid().ToString("N"))), 4, 8);
                text = text.Replace("-", "").ToLower();
                return text;
            }
        }
    }
}
