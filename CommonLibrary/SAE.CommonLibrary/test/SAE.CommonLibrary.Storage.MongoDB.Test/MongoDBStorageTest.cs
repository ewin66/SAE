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
        /// 添加数据至Mongodb中
        /// </summary>
        [Fact]
        public ClassGrade Add()
        {
            var classGrade = new ClassGrade();
            classGrade.Id = Guid.NewGuid().ToString();
            //此处设置了默认Id为10
            _storage.Add(classGrade);
            var grade = this._storage.Find<ClassGrade>(classGrade.Id);
            Assert.True(_storage.AsQueryable<ClassGrade>()
                                .Count(s => s.Id == classGrade.Id) == 1);
            Assert.NotNull(grade);
            return grade;
        }

        /// <summary>
        /// 更新
        /// </summary>
        [Fact]
        public void Update()
        {
            var classGrade= this.Add();
            //此处设置了默认Id为10
            classGrade.Students = new List<Student>
            {
                new Student
                {
                     Age=100,
                     Name="呵呵",
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
        [InlineData("大禹")]
        public void Query(string name)
        {
            this.Add();
            //通过Linq来查询复杂的对象
            var count = _storage.AsQueryable<ClassGrade>()
                                .Where(s => s.Name== name)
                                .Count();
            //等效于下面这种方式
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
            //移除   注：暂不支持Clear
            _storage.Remove(classGrade);
            Assert.True(_storage.AsQueryable<ClassGrade>()
                                .Count(s => s.Id == classGrade.Id)==0);
        }
    }
}
