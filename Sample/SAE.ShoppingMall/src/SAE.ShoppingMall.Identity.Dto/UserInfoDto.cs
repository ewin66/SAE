using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Dto
{
    public class UserInfoDto
    {
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 故乡
        /// </summary>
        public string Hometown { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

    }
}
