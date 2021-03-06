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
            this.Credentials = new Credentials();
            this.Information = new UserInfo();
            this.Roles = new List<string>();
        }

        public User(Credentials credentials):this()
        {
            this.Create(credentials);
            
        }
        /// <summary>
        /// 
        /// </summary>
        public override IIdentity Identity => this.Id.ToIdentity();
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
       
        
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

        public void Destory()
        {
            throw new NotImplementedException();
        }

        public void GrantRole(IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }
    }

    public partial class User
    {
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="roleProvider"></param>
        /// <param name="permissionProvider"></param>
        /// <returns></returns>
        public bool Authorize(string flag, Func<IIdentity, Role> roleProvider, Func<IIdentity, Permission> permissionProvider)
        {
            foreach (CommonLibrary.EventStore.Identity roleId in this.Roles)
            {
                var role = roleProvider.Invoke(roleId);
                if (role.Authorize(flag, permissionProvider)) return true;
            }
            return false;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="credentials">身份证明</param>
        public void Create(Credentials credentials)
        {
            this.Apply(new UserRegisterEvent
            {
                Id = Utils.GenerateId().ToString(),
                LoginName = credentials.Name,
                Password = credentials.Password,
                Salt = credentials.Salt,
                CreateTime=DateTime.Now,
                Status= (int)Status.Enable
            });

            this.ChangeInformation(new UserInfo(Sex.Woman,
                credentials.Name,string.Empty, DateTime.Now, string.Empty, new Contact()));
        }

        public void Change(User user)
        {
            this.ChangeInformation(user.Information);
        }
        

        public void ChangeInformation(UserInfo userInfo)
        {
            this.Apply(userInfo.To<UserChangeInfoEvent>());
            
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

    //public partial class User
    //{

    //    internal void When(UserRegisterEvent @event)
    //    {
    //        this.Id = @event.Id;
    //        this.Credentials = new Credentials(@event.LoginName, @event.Password, @event.Salt);
    //        this.CreateTime = @event.CreateTime;
    //        this.Status = @event.Status.EnumTo<Status>();
    //    }
    //    internal void When(UserChangeInfoEvent @event)
    //    {
    //        this.Information = new UserInfo(@event.Sex.EnumTo<Sex>(),
    //                                        @event.Name,
    //                                        @event.Icon,
    //                                        @event.BirthDate,
    //                                        @event.Hometown,
    //                                        new Contact
    //                                        {
    //                                            Email = @event.Email,
    //                                            Phone = @event.Phone,
    //                                            QQ = @event.QQ
    //                                        });
    //    }
    //}
}
