using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public CredentialsDto Credentials { get; set; }
        public int Status { get; set; }
        public UserInfoDto Information { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
