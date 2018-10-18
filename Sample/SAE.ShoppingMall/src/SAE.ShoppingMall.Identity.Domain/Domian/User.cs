using System;
using SAE.CommonLibrary.EventStore;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Domain.ValueObject;
using SAE.ShoppingMall.Infrastructure;
using System.Linq;
using System.Collections.Generic;
using SAE.CommonLibrary.Common;

namespace SAE.ShoppingMall.Identity.Domain
{
    public partial class User : AggregateRoot
    {
        public User()
        {
            
        }

        public User(Credentials credentials)
        {
            this.Create(credentials);
            this.Roles = new List<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        public override IIdentity Identity => (CommonLibrary.EventStore.Identity)Id;
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

        public ICollection<string> Roles { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="input"></param>
        /// <param name="roleProvider"></param>
        /// <param name="permissionProvider"></param>
        /// <returns></returns>
        public bool Authorize(string input,Func<IIdentity,Role> roleProvider,Func<IIdentity,Permission> permissionProvider)
        {
            foreach(CommonLibrary.EventStore.Identity roleId in this.Roles)
            {
                var role = roleProvider.Invoke(roleId);
                if (role.Authorize(input, permissionProvider)) return true;
            }
            return false;
        }

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
                Id = Utility.GenerateId().ToString(),
                LoginName = credentials.Name,
                Password = credentials.Password,
                Salt = credentials.Salt,
                CreateTime=DateTime.Now,
                Status= (int)Status.Enable
            });
            this.ChangeInformation(new UserInfo(Sex.Woman, DateTime.Now, "", new Contact()));
        }

        public void ChangeInformation(UserInfo userInfo)
        {
            this.Apply(new ChangeUserInfoEvent
            {
                BirthDate = userInfo.BirthDate,
                Email = userInfo.Contact.Email,
                Hometown = userInfo.Hometown,
                Phone = userInfo.Contact.Phone,
                QQ = userInfo.Contact.QQ,
                Sex = (int)userInfo.Sex,
            });
            
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password)
        {
            return this.Credentials.Verify(password);
        }
    }

    public partial class User
    {

        internal void When(RegisterUserEvent @event)
        {
            this.Id = @event.Id;
            this.Credentials = new Credentials(@event.LoginName, @event.Password, @event.Salt);
            this.Name = this.Credentials.Name;
            this.CreateTime = @event.CreateTime;
            this.Status = Utils.EnumTo<Status>(@event.Status);
            
        }
        internal void When(ChangeUserInfoEvent @event)
        {
            this.Information = new UserInfo(Utils.EnumTo<Sex>(@event.Sex),
                                            @event.BirthDate,
                                            @event.Hometown,
                                            new Contact
                                            {
                                                Email = @event.Email,
                                                Phone = @event.Phone,
                                                QQ = @event.QQ
                                            });
        }
    }
}
