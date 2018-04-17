using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.EventStore.Document.Memory.Test.Domain;
using SAE.Test.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace SAE.CommonLibrary.EventStore.Document.Memory.Test
{
    public class DocumentStoreTest:BaseTest
    {
        private readonly IDocumentStore _documentStore;
        public DocumentStoreTest(ITestOutputHelper testOutputHelper):base(testOutputHelper)
        {
            this._documentStore = Unit.GetProvider(s => s.AddMemberDocument())
                                    .GetService<IDocumentStore>();
        }

        [Theory]
        [InlineData("mypjb1994","Aa123456")]
        public User Register(string loginName,string password)
        {
            var user = new User();
            user.Create(loginName, password);
            _documentStore.Save(user);
            this.Show(user);
            return user;
        }
        [Theory]
        [InlineData("Aa123456","111111")]
        public void ChangePassword(string originalPassword, string password)
        {
            var user = this.Register($"ChangeUser_{new object().GetHashCode()}", originalPassword);
            user = _documentStore.Find<User>(new Identity(user.Id));
            user.ChangePassword(originalPassword, password);
            _documentStore.Save(user);
        }

        [Theory]
        [InlineData(1,"pjb")]
        public void ChangeProperty(int sex,string name)
        {
            var number = new object().GetHashCode();
            var user= this.Register($"changePropertyTest_{number}", "Aa123456");

            user = _documentStore.Find<User>(user.Identity);
            user.SetProperty(name,sex);
            _documentStore.Save(user);
            var newUser = _documentStore.Find<User>(user.Identity);
            Assert.True(newUser.Sex == user.Sex &&
                        newUser.LoginName == user.LoginName &&
                        newUser.Name == user.Name &&
                        newUser.Password == user.Password);
            this.Show(user);
        }
        
    }
}
