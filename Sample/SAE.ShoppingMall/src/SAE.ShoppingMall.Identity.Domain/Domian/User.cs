using System;
using SAE.CommonLibrary.EventStore;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Domain.ValueObject;

namespace SAE.ShoppingMall.Identity.Domain
{
    public partial class User : AggregateRoot
    {
        public User()
        {
            
        }

        public override IIdentity Identity => new CommonLibrary.EventStore.Identity(this.Id.ToString());
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
        ///// <summary>
        ///// 标识
        ///// </summary>
        //public IIdentity Identity { get => new EventStore.Identity(this.Id.ToString()); }
        
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Icon { get; set; }
        
        /// <summary>
        /// 详细信息
        /// </summary>
        public UserInfo Information { get; set; }
        /// <summary>
        /// 身份证明
        /// </summary>
        public Credentials Credentials { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
    }

    public partial class User
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="credentials">身份证明</param>
        public void Create(Credentials credentials)
        {
            this.Apply(new RegisterUserEvent
            {
                Id = Guid.NewGuid().ToString(),
                LoginName = credentials.Name,
                Password = credentials.Password,
                Salt = credentials.Salt
            });
        }
    }

    public partial class User
    {
       
        internal void When(RegisterUserEvent registerUser)
        {
            this.Id = registerUser.Id;
            this.Credentials = new Credentials(registerUser.LoginName, registerUser.Password, registerUser.Salt);
            this.Name = this.Credentials.Name;
            this.Information = new UserInfo(Sex.Woman, DateTime.Now, "", new Contact());
        }
    }
}
