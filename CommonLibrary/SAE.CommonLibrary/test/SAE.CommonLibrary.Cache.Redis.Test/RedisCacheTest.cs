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
            Assert.True(_cache.Add(student.Name, student), "�������ʧ��");
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

            Assert.True(_cache.Add(dic, DateTime.Now.AddSeconds(60)), "�������ʧ��");
        }
        [Fact]
        public void Get()
        {
            this.Add();
            var student = _cache.Get<Student>("pjb");
            Assert.True(student?.Name == "pjb", "�����ȡ����");
        }

        [Fact]
        public void GetValues()
        {
            this.Adds();
            var keys = _cache.GetKeys();
            Assert.True(keys.Count() > 0, "keys��ȡʧ��");
            var models = _cache.GetMore<Student>(keys);
            Assert.True(keys.Count() > 0, "Value��ȡʧ��");
        }
        [Fact]
        public void Remove()
        {
            this.Add();
            Assert.True(_cache.Remove("pjb"), "key=pjb,�Ƴ�ʧ��");
            Assert.Null(_cache.Get<Student>("pjb"));
        }
        [Fact]
        public void RemoveAll()
        {
            this.Adds();
            Thread.Sleep(1000);
            var keys = _cache.GetKeys();
            Assert.True(keys.Count() > 0, "keys��ȡʧ��");
            Assert.True(_cache.Remove(keys.ToArray()), "һ�����Ƴ����ʧ��");
            Assert.True(_cache.GetKeys().Count() == 0, "��ջ���ʧ��");
        }
        [Fact]
        public void Clear()
        {
            _cache.Clear();
            //Assert.True(_cache.GetKeys().Count() == 0, "��ջ���ʧ��");
        }

        public void Dispose()
        {
            this._cache.Dispose();
        }
    }
}
