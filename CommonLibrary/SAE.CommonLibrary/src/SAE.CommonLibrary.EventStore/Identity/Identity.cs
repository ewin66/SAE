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
            this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Identity(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
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

    }
}
