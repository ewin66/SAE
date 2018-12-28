using System;
using System.Collections.Generic;
using System.Text;
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

        }
        public App(string name,ClientCredentials clientCredentials, SignEndpoint signEndpoint)
        {
            this.Create(name, clientCredentials, signEndpoint);
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
        public void Create(string name,ClientCredentials credentials,SignEndpoint signEndpoint)
        {
            this.Apply(new RegisterAppEvent
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
                this.ChangeName(app.Name);
            if (this.Status != app.Status)
                this.ChangeStatus(app.Status);
        }

        public void ChangeEndopint(SignEndpoint signEndpoint)
        {
            this.Apply(new ChangeAppEndpointEvent
            {
                Signin = signEndpoint.Signin,
                Signout = signEndpoint.Signout
            });
        }

        public void ChangeName(string name)
        {
            this.Apply(new ChangeAppNameEvent
            {
                 Name=name
            });
        }

        public void ChangeStatus(Status status)
        {
            this.Apply(new ChangeAppStatusEvent
            {
                Status = Status
            });
        }

        

        public void ChangeSecret(string secret)
        {
            this.Apply(new ChangeAppSecretEvent
            {
                Secret = secret
            });
        }
    }

    public partial class App
    {
        internal void When(RegisterAppEvent @event)
        {
            this.Client = new ClientCredentials(@event.Id, @event.Secret);
            this.Endpoint = new SignEndpoint(@event.Signin, @event.Signout);
            this.Name = this.Name;
            this.CreateTime = @event.CreateTime;
            this.Status = @event.Status;
        }

        internal void When(ChangeAppEndpointEvent @event)
        {
            this.Endpoint = new SignEndpoint(@event.Signin,@event.Signout);
        }

        internal void When(ChangeAppNameEvent @event)
        {
            this.Name = @event.Name;
        }

        internal void When(ChangeAppSecretEvent @event)
        {
            this.Client.Secret = @event.Secret;
        }
        internal void When(ChangeAppStatusEvent @event)
        {
            this.Status = @event.Status;
        }
    }
}
