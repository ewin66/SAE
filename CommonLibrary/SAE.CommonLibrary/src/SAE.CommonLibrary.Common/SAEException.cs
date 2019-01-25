using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SAE.CommonLibrary.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SAEException:Exception
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public StatusCode Code { get; }
        /// <summary>
        /// 
        /// </summary>
        public SAEException()
        {
            this.Code = StatusCode.Custom;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public SAEException(StatusCode code):this(code.Display())
        {
            this.Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public SAEException(StatusCode code,string message):this(message)
        {
            this.Code = code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="exception"></param>
        public SAEException(StatusCode code,Exception exception):this(code,code.Display(),exception)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public SAEException(StatusCode code,string message,Exception exception):this(message,exception)
        {
            this.Code = code;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SAEException(string message):base(message)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SAEException(string message, Exception innerException):base(message,innerException)
        {
        }
    }
}
