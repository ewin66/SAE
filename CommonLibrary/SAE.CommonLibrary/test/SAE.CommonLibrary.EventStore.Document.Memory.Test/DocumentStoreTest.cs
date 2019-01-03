using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.EventStore.Document.Memory.Test.Domain;
using SAE.CommonLibrary.EventStore.Document.Memory.Test.Event;
using SAE.CommonLibrary.EventStore.Queryable;
using SAE.CommonLibrary.EventStore.Queryable.Builder;
using SAE.CommonLibrary.MQ;
using SAE.Test.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace SAE.CommonLibrary.EventStore.Document.Memory.Test
{
    public class DocumentStoreTest:BaseTest
    {
        private readonly IDocumentStore _documentStore;
        private readonly IMQ _mq;
        private readonly IPersistenceService _persistenceService;
        public DocumentStoreTest(ITestOutputHelper testOutputHelper):base(testOutputHelper)
        {
            var serviceProvider = Unit.GetProvider(s => s.AddMemberDocument()
                                                         .AddMemoryPersistenceService()
                                                         .AddMemoryMQ()
                                                         .AddDefaultHandler()
                                                         .AddLogger());
                                                         
            serviceProvider.UseDefaultDocumentPublish();

            this._documentStore=serviceProvider.GetService<IDocumentStore>();
            
            this._mq = serviceProvider.GetService<IMQ>();

            this._persistenceService = serviceProvider.GetService<IPersistenceService>();
            
            
            
        }

        private void Initial(int i)
        {
            var builder= this._mq.CreateBuilder()
                            .RegisterAssembly();
            switch (i)
            {
                case 1:
                    {
                        builder.Mapping<User>()
                               .Mapping(HandlerEnum.Update, t =>
                               {
                                   return t.Name.StartsWith("Change") || t.Name.StartsWith("Set");
                               })
                               .Mapping();
                        break;
                    }
                default:
                    {
                        builder.Mapping<User, ChangePasswordEvent>(HandlerEnum.Update)
                               .Mapping<User, CreateEvent>()
                               .Mapping<User, UpdateEvent>();
                        break;
                    }
            }
            

            builder.Build();
        }

        [Theory]
        [InlineData("mypjb1994","Aa123456",0)]
        [InlineData("mypjb1994", "Aa123456",1)]
        public User Register(string loginName,string password,int stage=0)
        {
            this.Initial(stage);
            var user = new User();
            user.Create(loginName, password);
            _documentStore.Save(user);
            this.Show(user);
            var newUser = this._persistenceService.Find<User>(user.Id);
            Assert.NotNull(newUser);
            Assert.Equal(user.Id, newUser.Id);
            Assert.Equal(user.LoginName, newUser.LoginName);
            Assert.Equal(user.Name, newUser.Name);
            Assert.Equal(user.Password, newUser.Password);
            Assert.Equal(user.Sex, newUser.Sex);
            Assert.NotEqual(user.Version, newUser.Version);
            this.Show(newUser);
            return user;
        }

        [Theory]
        [InlineData("Aa123456","111111",0)]
        [InlineData("Aa123456", "111111",1)]
        public void ChangePassword(string originalPassword, string password, int stage)
        {
            this.Initial(stage);
            var user = this.Register($"ChangeUser_{new object().GetHashCode()}", originalPassword);
            user = _documentStore.Find<User>(user.Identity);
            user.ChangePassword(originalPassword, password);
            _documentStore.Save(user);
            var newUser = this._persistenceService.Find<User>(user.Id);
            Assert.NotNull(newUser);
            Assert.Equal(user.Id, newUser.Id);
            Assert.Equal(user.LoginName, newUser.LoginName);
            Assert.Equal(user.Name, newUser.Name);
            Assert.Equal(user.Password, newUser.Password);
            Assert.Equal(user.Sex, newUser.Sex);
            Assert.NotEqual(user, newUser);
            this.Show(newUser);
        }

        [Theory]
        [InlineData(1,"pjb",0)]
        [InlineData(1, "pjb",1)]
        public void ChangeProperty(int sex,string name, int stage)
        {
            this.Initial(stage);
            var number = new object().GetHashCode();
            var user= this.Register($"changePropertyTest_{number}", "Aa123456");

            user = _documentStore.Find<User>(user.Identity);
            user.SetProperty(name,sex);
            _documentStore.Save(user);
            var newUser = this._persistenceService.Find<User>(user.Id);
            Assert.NotNull(newUser);
            Assert.Equal(user.Id, newUser.Id);
            Assert.Equal(user.LoginName, newUser.LoginName);
            Assert.Equal(user.Name, newUser.Name);
            Assert.Equal(user.Password, newUser.Password);
            Assert.Equal(user.Sex, newUser.Sex);
            Assert.NotEqual(user, newUser);
            this.Show(newUser);
        }
        
    }
}
