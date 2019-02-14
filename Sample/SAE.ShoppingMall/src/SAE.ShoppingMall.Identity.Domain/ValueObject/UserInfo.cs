using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.ValueObject
{
    public class UserInfo
    {
        public UserInfo()
        {
            this.Contact = new Contact();
        }
        public UserInfo(Sex sex,string name,string icon,DateTime birthDate,string hometown,Contact contact):this()
        {
            this.Sex = Sex;
            this.BirthDate = birthDate;
            this.Hometown = hometown;
            this.Contact = contact;
            this.Icon = icon;
            this.Name = name;
        }

        public string Name { get; set; }
        public string Icon { get; set; }

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
