using System;
using System.Text.RegularExpressions;
using SAE.CommonLibrary.EventStore;

namespace SAE.ShoppingMall.Identity.Domain
{
    public class Permission: AggregateRoot
    {
        public Permission()
        {
            
        }
        public override IIdentity Identity => throw new NotImplementedException();
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模式
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// 比对两者是否匹配
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Comparison(string input)
        {
            return Regex.Match(input, this.Pattern).Success;
        }
    }
}
