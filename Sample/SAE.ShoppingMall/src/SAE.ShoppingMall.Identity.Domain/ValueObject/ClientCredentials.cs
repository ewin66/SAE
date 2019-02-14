using System;
using System.Collections.Generic;
using System.Text;
using SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.EventStore;

namespace SAE.ShoppingMall.Identity.Domain.ValueObject
{
    /// <summary>
    /// credentials
    /// </summary>
    public class ClientCredentials
    {
        public ClientCredentials():this(IdentityGenerator.Build().ToString(), IdentityGenerator.Build().ToString())
        {

        }
        public ClientCredentials(string secret):this(IdentityGenerator.Build().ToString(), secret)
        {

        }
        public ClientCredentials(string id,string secret)
        {
            if (string.IsNullOrWhiteSpace(id))
                id = IdentityGenerator.Build().ToString();
            if (string.IsNullOrWhiteSpace(secret))
                secret = IdentityGenerator.Build().ToString();
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
