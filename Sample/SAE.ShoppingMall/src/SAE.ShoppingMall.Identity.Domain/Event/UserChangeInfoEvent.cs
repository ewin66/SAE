using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class UserChangeInfoEvent:Event
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Sex { get; set; }
       
        public DateTime BirthDate { get; set; }
       
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
