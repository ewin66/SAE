using System;
using SAE.CommonLibrary.EventStore;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class RegisterUserEvent : Event
    {
        public string Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreateTime { get;  set; }
        public int Status { get;  set; }
    }
}
