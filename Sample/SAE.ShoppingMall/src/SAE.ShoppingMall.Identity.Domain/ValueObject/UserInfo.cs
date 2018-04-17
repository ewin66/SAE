using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.ValueObject
{
    public class UserInfo
    {
        public UserInfo()
        {

        }
        public UserInfo(Sex sex,DateTime birthDate,string hometown,Contact contact)
        {
            this.Sex = Sex;
            this.BirthDate = birthDate;
            this.Hometown = hometown;
            this.Contact = contact;
        }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 故乡
        /// </summary>
        public string Hometown { get; set; }

        public Contact Contact { get; set; }
        
    }
}
