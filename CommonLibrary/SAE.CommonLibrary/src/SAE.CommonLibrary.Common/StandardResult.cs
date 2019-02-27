using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.Common
{
    /// <summary>
    /// 标准输出
    /// </summary>
    public class StandardResult
    {
        /// <summary>
        /// 
        /// </summary>
        public StandardResult():this(StatusCode.Success)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        public StandardResult(object body):this()
        {
            this.Body = body;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public StandardResult(StatusCode code):this(code,string.Empty)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public StandardResult(StatusCode code,string message)
        {
            this.StatusCode = code;
            this.message = message;
        }
        /// <summary>
        /// 状态码
        /// </summary>
        public StatusCode StatusCode { get; set; }
        private string message;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get
            {
                return this.message.IsNullOrWhiteSpace() ? this.StatusCode.ToString() : this.message;
            }
            set
            {
                this.message = value;
            }
        }
        /// <summary>
        /// 主体
        /// </summary>
        public object Body { get; set; }
    }
    /// <summary>
    /// <seealso cref="StandardResult"/>的泛型实现
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class StandardResult<T> : StandardResult
    {
        /// <summary>
        /// 
        /// </summary>
        public StandardResult()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        public StandardResult(T body) : base(body)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public StandardResult(StatusCode code) : base(code)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public StandardResult(StatusCode code, string message) : base(code, message)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public new T Body
        {
            get => (T)base.Body;
            set => base.Body = value;
        }
    }
}
