using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.Ioc.ServiceCollections;
using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.Log;
using SAE.Test.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace SAE.CommonLibrary.Ioc.Test
{
    public class ServiceCollectionTest:BaseTest
    {
        private readonly IServiceCollection _services;

        public ServiceCollectionTest(ITestOutputHelper output) : base(output)
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void AddRelative()
        {
            var provider = this._services.AddRelative()
                               .BuildServiceProvider();

            var jsonConvertor = provider.GetService<IJsonConvertor>();
            var log = provider.GetService<ILog<object>>();
            var st = provider.GetService<IT<Student>>();
            
            var json= jsonConvertor.Serialize(new Student());
            this.Show(json);
            this.Show(log.ToString());
            this.Show(st.ToString());
        }

       


       
    }
    public class Student
    {
        public Student()
        {
            this.Name = "PJB";
            this.Age = 10;
        }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public interface IT<T> where T : Student
    {

    }

    public class A<T> : IT<T> where T : Student
    {

    }

}
