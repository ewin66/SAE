using System;
using System.Collections.Generic;
using System.Text;
using SAE.CommonLibrary.Common.Check;

namespace SAE.ShoppingMall.Identity.Domain.ValueObject
{
    /// <summary>
    /// credentials
    /// </summary>
    public class ClientCredentials
    {
        public ClientCredentials():this(Guid.NewGuid().ToString(),Guid.NewGuid().ToString())
        {

        }
        public ClientCredentials(string secret):this(Guid.NewGuid().ToString(),secret)
        {

        }
        public ClientCredentials(string id,string secret)
        {
            Assert.Build(id)
                  .NotNullOrWhiteSpace();
            Assert.Build(secret)
                  .NotNullOrWhiteSpace();
            this.Id = id;
            this.Secret = secret;
        }
        /// <summary>
        /// app id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// app secret
        /// </summary>
        public string Secret { get; set; }
    }
}
