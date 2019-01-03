using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SAE.Test.Infrastructure
{
    public class Unit
    {
        //public static readonly IServiceCollection ServiceCollection;

        public static IServiceProvider GetProvider(Action<IServiceCollection> action)
        {
            var serviceCollection = new ServiceCollection();
            action(serviceCollection);
            return serviceCollection.BuildServiceProvider()
                                    .UseServiceFacade();
        }
        static Unit()
        {
            //serviceCollection = new ServiceCollection();
            //var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            //var builder = new ConfigurationBuilder()
            //                    .AddJsonFile(path);
            //var configuration = builder.Build();
            //var client = configuration.GetSection("DependencyInjection");
            //serviceCollection.AddDependencyInjectionConfiguration(configuration.GetSection("DependencyInjection"));
            //InitialEvent?.Invoke
            //Provider=serviceCollection.BuildServiceProvider();
        }

        //public static event Action<IServiceCollection> InitialEvent;

    }


    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        Nav,
        Man
    }

    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        public Student()
        {
            this.CreateTime = DateTime.Now;
        }
        public DateTime CreateTime { get; set; }
        public string Name
        {
            get; set;
        }
        public int Age
        {
            get; set;
        }
        public Sex Sex
        {
            get; set;
        }
    }
    /// <summary>
    /// 班级
    /// </summary>
    public class ClassGrade
    {
        public DateTime CreateTime { get; set; }
        public string Id { get; set; }
        public ClassGrade()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = "大禹";
            Students = new List<Student>
            {
                new Student
                {
                     Age=22,
                      Name="pjb",
                       Sex= Sex.Man
                },
                new Student
                {
                     Age=23,
                      Name="cwj",
                       Sex= Sex.Man
                },
                new Student
                {
                     Age=22,
                      Name="oybh",
                       Sex= Sex.Nav
                },

            };
            this.CreateTime = DateTime.Now;
        }
        public string Name
        {
            get; set;
        }
        public IEnumerable<Student> Students { get; set; }

        public IEnumerator GetEnumerator()
        {
            return this.Students?.GetEnumerator();
        }
    }

}
