using System;
using System.Collections.Generic;
using System.Text;
using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.EventStore;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Domain.ValueObject;

namespace SAE.ShoppingMall.Identity.Domain
{
    /// <summary>
    /// 应用
    /// </summary>
    public partial class App : AggregateRoot
    {
        [Obsolete()]
        public App()
        {
            this.Client = new ClientCredentials();
            this.Endpoint = new SignEndpoint();
        }
        public App(string name, SignEndpoint signEndpoint) : this(name,new ClientCredentials(),signEndpoint)
        {
            
        }

        public App(string name,ClientCredentials credentials, SignEndpoint signEndpoint) : this()
        {
            this.Create(name, credentials, signEndpoint);
        }

        public override IIdentity Identity => IdentityGenerator.Build(this.Client.Id);

        /// <summary>
        /// 客户端凭证
        /// </summary>
        public ClientCredentials Client { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 回调终端
        /// </summary>
        public SignEndpoint Endpoint { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
    }

    public partial class App
    {
        public void Create(string name, ClientCredentials credentials, SignEndpoint signEndpoint)
        {
            this.Apply(new AppCreateEvent
            {
                Id = credentials.Id,
                Name = name,
                Secret = credentials.Secret,
                Signin = signEndpoint.Signin,
                Signout = signEndpoint.Signout,
                CreateTime = DateTime.Now,
                Status = Status.Enable
            });
        }
        public void Change(App app)
        {
            if (this.Client != app.Client)
                this.ChangeSecret(app.Client.Secret);
            if (this.Name != app.Name)
                this.ChangeName(app.Name);
            if (this.Endpoint != app.Endpoint)
                this.ChangeEndopint(app.Endpoint);
            if (this.Status != app.Status)
                this.ChangeStatus(app.Status);
        }

        public void ChangeEndopint(SignEndpoint signEndpoint)
        {
            this.Apply(new AppChangeEndpointEvent
            {
                Signin = signEndpoint.Signin,
                Signout = signEndpoint.Signout
            });
        }

        public void ChangeName(string name)
        {
            this.Apply(new AppChangeNameEvent
            {
                Name = name
            });
        }

        public void ChangeStatus(Status status)
        {
            this.Apply(new AppChangeStatusEvent
            {
                Status = Status
            });
        }



        public void ChangeSecret(string secret)
        {
            this.Apply(new AppChangeSecretEvent
            {
                Secret = secret
            });
        }

        public void Remove()
        {
            this.Apply(this.To<AppRemoveEvent>());
        }
    }

    public partial class App
    {
        internal void When(AppCreateEvent @event)
        {
            this.Client = new ClientCredentials(@event.Id, @event.Secret);
            this.Endpoint = new SignEndpoint(@event.Signin, @event.Signout);
            this.Name = this.Name;
            this.CreateTime = @event.CreateTime;
            this.Status = @event.Status;
        }

        internal void When(AppChangeEndpointEvent @event)
        {
            this.Endpoint = new SignEndpoint(@event.Signin, @event.Signout);
        }

        internal void When(AppChangeNameEvent @event)
        {
            this.Name = @event.Name;
        }

        internal void When(AppChangeSecretEvent @event)
        {
            this.Client.Secret = @event.Secret;
        }
        internal void When(AppChangeStatusEvent @event)
        {
            this.Status = @event.Status;
        }
    }
}
