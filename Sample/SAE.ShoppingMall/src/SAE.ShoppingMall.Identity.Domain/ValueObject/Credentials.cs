using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.ValueObject
{
    public class Credentials
    {
        public Credentials()
        {

        }
        public Credentials(string loginName, string password)
        {
            this.Name = loginName;
            this.Password = password;
        }
        public Credentials(string loginName,string password,string salt):this(loginName,password)
        {
            this.Salt = salt;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 加密盐
        /// </summary>
        public string Salt { get; set; }

        internal void Encrypt()
        {
            
        }
    }
}
