using Microsoft.Extensions.DependencyInjection;
using SAE.Test.Infrastructure;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4, DisableTestParallelization = false)]

namespace SAE.CommonLibrary.MQ.RabbitMQ.Test
{

    public class TestConfig : MQConfig
    {
        public TestConfig()
        {
            this.Url = "amqp://sae:123456@rabbitmq.mq.com:12004/sae";
            this.Query = "text";
        }
    }

    public class MQTest : BaseTest, IDisposable
    {
        static MQTest()
        {
            //Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.WriteLine(Console.OutputEncoding);
        }
        private readonly IMQ _mq;

        public MQTest(ITestOutputHelper output) : base(output)
        {

            //_mq = Unit.GetProvider(s => s.AddMQ(new TestConfig()))
            //          .GetService<IMQ>();

            _mq = Unit.GetProvider(s => s.AddMemoryMQ())
                      .GetService<IMQ>();

        }

        public void Dispose()
        {
            _mq.Dispose();
        }

        [Fact]
        public async Task PublishMessage()
        {
            

            await _mq.SubscibeAsync<ClassGrade>(cg =>
            {
                this.WriteLine($"Test1_{nameof(ClassGrade)}:{cg.Id}");
            });
            await _mq.SubscibeAsync<Student>(s =>
            {
                this.WriteLine($"1:{nameof(Student)}:name={s.Name},sex={s.Sex},age={s.Age}");
            });
            await _mq.SubscibeAssemblyAsync();
            _mq.SubscibeAssembly()
               .SubscibeType<SudentHandle>();
            await _mq.SubscibeTypeAsync<SudentHandle>();
            Init();
            Thread.Sleep(5 * 1000);

        }


        private void Init()
        {
            Task.Run(() =>
            {
                var i = 1;
                while (i-- > 0)
                {
                    _mq.PublishAsync(new Student
                    {
                        Age = new Object().GetHashCode() % 100,
                        Name = Guid.NewGuid().ToString(),
                        Sex = Sex.Nav
                    });
                    _mq.PublishAsync(new ClassGrade());
                }
            });
        }

    }



   
    public class SudentHandle : IHandler<Student>
    {
        private readonly ITestOutputHelper _output;
        public SudentHandle(ITestOutputHelper output)
        {
            _output = output;
        }
        public void Handle(Student message)
        {
            _output.WriteLine($"{nameof(SudentHandle)}:{nameof(Student)}:name={message.Name},sex={message.Sex},age={message.Age}");
        }
    }

}
