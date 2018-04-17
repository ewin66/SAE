using System;
using SAE.Test.Infrastructure;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SAE.CommonLibrary.Cache.Redis.Test
{
    public class RedisConfigTest:RedisConfig
    {
        public RedisConfigTest()
        {
            this.Connection = "redis.cache.com:6379,allowadmin=true,syncTimeout=5000";
            this.DB = 2;
        }
    }
    public class RedisCacheTest:IDisposable
    {
        private readonly ICache _cache;
        public RedisCacheTest()
        {
            _cache = Unit.GetProvider(s => s.AddCache(new RedisConfigTest()))
                         .GetService<ICache>();
        }
        [Fact]
        public void Add()
        {
            var student = new Student() { Age=24, Name="pjb", Sex= Sex.Man};
            Assert.True(_cache.Add(student.Name, student), "缓存加入失败");
        }
        [Fact]
        public void Adds()
        {
            Dictionary<string, Student> dic = new Dictionary<string, Student>();
            var i = 0;
            while (i <= 1000)
            {
                dic[i.ToString()] = new Student
                {
                    Age = 22,
                    Name = i.ToString(),
                };
                ++i;
            }

            Assert.True(_cache.Add(dic, DateTime.Now.AddSeconds(60)), "缓存加入失败");
        }
        [Fact]
        public void Get()
        {
            this.Add();
            var student = _cache.Get<Student>("pjb");
            Assert.True(student?.Name == "pjb", "缓存获取有误");
        }

        [Fact]
        public void GetValues()
        {
            this.Adds();
            var keys = _cache.GetKeys();
            Assert.True(keys.Count() > 0, "keys获取失败");
            var models = _cache.GetMore<Student>(keys);
            Assert.True(keys.Count() > 0, "Value获取失败");
        }
        [Fact]
        public void Remove()
        {
            this.Add();
            Assert.True(_cache.Remove("pjb"), "key=pjb,移除失败");
            Assert.Null(_cache.Get<Student>("pjb"));
        }
        [Fact]
        public void RemoveAll()
        {
            this.Adds();
            Thread.Sleep(1000);
            var keys = _cache.GetKeys();
            Assert.True(keys.Count() > 0, "keys获取失败");
            Assert.True(_cache.Remove(keys.ToArray()), "一次性移除多个失败");
            Assert.True(_cache.GetKeys().Count() == 0, "清空缓存失败");
        }
        [Fact]
        public void Clear()
        {
            _cache.Clear();
            //Assert.True(_cache.GetKeys().Count() == 0, "清空缓存失败");
        }

        public void Dispose()
        {
            this._cache.Dispose();
        }
    }
}
