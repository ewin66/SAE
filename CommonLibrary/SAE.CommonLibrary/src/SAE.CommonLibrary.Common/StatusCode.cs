using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAE.CommonLibrary.Common
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Display(Name = "成功")]
        Success = 0,
        /// <summary>
        /// 未知的异常
        /// </summary>
        [Display(Name = "未知的异常")]
        Unknown = -1,
        /// <summary>
        /// 自定义的异常
        /// </summary>
        [Display(Name = "自定义的异常")]
        Custom =-2,
    }
}
