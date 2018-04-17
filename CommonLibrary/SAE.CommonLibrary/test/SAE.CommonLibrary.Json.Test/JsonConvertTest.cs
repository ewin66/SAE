using SAE.Test.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SAE.CommonLibrary.Json.Test
{
    public class JsonConvertTest:BaseTest
    {
        private readonly IJsonConvertor _jsonConvertor;
        
        public JsonConvertTest(ITestOutputHelper testOutputHelper):base(testOutputHelper)
        {
            this._jsonConvertor = new Imp.JsonConvertor();
        }
        [Fact]
        public void Serialize()
        {
            var json = this._jsonConvertor.Serialize(new Student { Name = "PJB", Sex = Sex.Man, Age = 23, Birthday = new DateTime(1994, 2, 27) });
            this.Show(json);
        }

        [Theory]
        [InlineData(@"{'name':'PJB','sex':1,'age':23,'birthday':'1994 - 02 - 27T00: 00:00'}")]
        [InlineData("{\"name\":\"PJB\",\"sex\":1,\"age\":23,\"birthday\":\"1994 - 02 - 27T00: 00:00\"}")]
        [InlineData(@"{'Name':'PJB','Sex':1,'Age':23,'Birthday':'1994 - 02 - 27T00: 00:00'}")]
        public void Deserialize(string json)
        {
            var student = this._jsonConvertor.Deserialize<Student>(json);
            
            Assert.True(student.Name == "PJB" &&
                        student.Sex == Sex.Man &&
                        student.Age == 23 &&
                        student.Birthday == new DateTime(1994, 2, 27));

            student = this._jsonConvertor.Deserialize(json, typeof(Student)) as Student;
            Assert.True(student.Name == "PJB" &&
                        student.Sex == Sex.Man &&
                        student.Age == 23 &&
                        student.Birthday == new DateTime(1994, 2, 27));

        }

    }

    public class Student
    {
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
    }
    public enum Sex
    {
        Woman,
        Man,

    }
}
