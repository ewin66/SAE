using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SAE.CommonLibrary.Common
{
    public class SAEException:Exception
    {
        public SAEException()
        {
        }

        protected SAEException(SerializationInfo info, StreamingContext context):base(info,context)
        {
        }

        public SAEException(string message):base(message)
        {
        }

        public SAEException(string message, Exception innerException):base(message,innerException)
        {
        }
    }
}
