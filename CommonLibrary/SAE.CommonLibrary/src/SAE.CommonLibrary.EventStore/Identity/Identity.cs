using SAE.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore
{
    /// <summary>
    /// 标识
    /// </summary>
    public class Identity:IIdentity
    {
        /// <summary>
        /// 具体值
        /// </summary>
        public string Id { get;  }
        /// <summary>
        /// 
        /// </summary>
        public Identity()
        {
            this.Id = Utils.GenerateId().ToString();
        }
        /// <summary>
        /// 根据<paramref name="id"/>构造一个<seealso cref="对象"/>
        /// </summary>
        /// <param name="id"></param>
        public Identity(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                id = Utils.GenerateId().ToString();
            this.Id = id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Id;
        }
        
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="identity"></param>
        public static implicit operator Identity(string identity)
        {
            return new Identity(identity);
        }

        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="identity"></param>
        public static implicit operator string(Identity identity)
        {
            return identity.ToString();
        }
    }
}
