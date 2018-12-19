using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Builder
{
    public class LimitRegistrationBuilder<TModel> : ILimitRegistrationBuilder<TModel> where TModel : class
    {
        internal LimitRegistrationBuilder(IRegistrationBuilder registrationBuilder)
        {
            this.RegistrationBuilder = registrationBuilder;
            this.ModelType = typeof(TModel);
        }

        public Type ModelType { get; }

        public IRegistrationBuilder RegistrationBuilder { get; }
    }
}
