using SAE.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace SAE.CommonLibrary.Storage.MongoDB.Test
{
    public class MongoDbConfigTest:MongoDBConfig
    {
        public MongoDbConfigTest()
        {
            this.Connection = "mongodb://mongodb.db.com:27017";
            this.DB = "test";
        }
    }
    public class MongoDBStorageTest
    {
        private readonly IStorage _storage;
        private readonly ITestOutputHelper _output;
        public MongoDBStorageTest(ITestOutputHelper output)
        {
            //_storage = Unit.GetProvider(s => s.AddStorage(new MongoDbConfigTest()))
            //               .GetService<IStorage>();
            _storage = Unit.GetProvider(s => s.AddMemoryStorage())
                           .GetService<IStorage>();
            _output = output;
        }

        /// <summary>
        /// ���������Mongodb��
        /// </summary>
        [Fact]
        public ClassGrade Add()
        {
            var classGrade = new ClassGrade();
            classGrade.Id = Guid.NewGuid().ToString();
            //�˴�������Ĭ��IdΪ10
            _storage.Add(classGrade);
            var grade = this._storage.Find<ClassGrade>(classGrade.Id);
            Assert.True(_storage.AsQueryable<ClassGrade>()
                                .Count(s => s.Id == classGrade.Id) == 1);
            Assert.NotNull(grade);
            return grade;
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Update()
        {
            var classGrade= this.Add();
            //�˴�������Ĭ��IdΪ10
            classGrade.Students = new List<Student>
            {
                new Student
                {
                     Age=100,
                     Name="�Ǻ�",
                     Sex=Sex.Nav
                }
            };
            _storage.Update(classGrade);
            var @class = _storage.AsQueryable<ClassGrade>()
                                 .First(s => s.Id == classGrade.Id);
            Assert.True(@class.Students.Count() == 1 &&
                    @class.Students.First().Name == classGrade.Students.First().Name);
        }

        [Theory]
        [InlineData("����")]
        public void Query(string name)
        {
            this.Add();
            //ͨ��Linq����ѯ���ӵĶ���
            var count = _storage.AsQueryable<ClassGrade>()
                                .Where(s => s.Name== name)
                                .Count();
            //��Ч���������ַ�ʽ
            //var query = from node in _storage.AsQueryable<ClassGrade>()
            //             where node.Id.Contains("10")
            //             select node;
            Assert.True(count > 0);
            _output.WriteLine(count.ToString());
        }

        [Fact]
        public void Remove()
        {
           var classGrade= this.Add();
            //�Ƴ�   ע���ݲ�֧��Clear
            _storage.Remove(classGrade);
            Assert.True(_storage.AsQueryable<ClassGrade>()
                                .Count(s => s.Id == classGrade.Id)==0);
        }
    }
}
