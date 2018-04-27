using System;
using System.Collections.Generic;
using System.Text;
using SAE.CommonLibrary.Common.Check;

namespace SAE.ShoppingMall.Identity.Domain.ValueObject
{
    /// <summary>
    /// 终端
    /// </summary>
    public class SignEndpoint
    {
        [Obsolete("orm调用",true)]
        public SignEndpoint()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signin"></param>
        /// <param name="signout"></param>
        public SignEndpoint(string signin,string signout)
        {
            Assert.Build(signin)
                  .NotNullOrWhiteSpace();

            Assert.Build(signout)
                  .NotNullOrWhiteSpace();

            this.Signin = signin;
            this.Signout = signout;
        }
        /// <summary>
        /// 登录
        /// </summary>
        public string Signin { get; set; }
        /// <summary>
        /// 登出
        /// </summary>
        public string Signout { get; set; }
    }
}
