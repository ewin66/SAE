using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Dto
{
    public class AppDto
    {

        public string Id
        {
            get { return this.AppId; }
            set
            {
                this.AppId = value;
            }
        }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public DateTime CreateTime { get; set; }
        public string Signin { get; set; }
        public string Signout { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
