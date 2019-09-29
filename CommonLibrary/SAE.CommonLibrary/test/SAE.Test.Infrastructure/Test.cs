using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Abstractions;
using Xunit;

namespace SAE.Test.Infrastructure
{
    public class Test : BaseTest
    {
        public Test(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Next()
        {
            var sv = new ServerConfig[]
            {
                new ServerConfig{Name="A", Weight = 4},
                new ServerConfig{Name="B", Weight = 2},
                new ServerConfig{Name="C", Weight = 1}
            };
            int index = 0;
            int sum = sv.Sum(m => m.Weight);
            for (int i = 0; i < sum; i++)
            {
                index = NextServerIndex(sv);
                this.WriteLine(string.Format("{0}{1}", sv[index].Name, sv[index].Weight));
            }
            //Enumerable.Range(0, 1000)
            //          .ToList()
            //          .ForEach(s =>
            //          {
            //              index = NextServerIndex(sv);
            //              Console.WriteLine("{0}{1}", sv[index].Name, sv[index].Weight);
            //          });
        }

        public int NextServerIndex(ServerConfig[] serverConfigArray)
        {
            int index = -1;
            int total = 0;
            int size = serverConfigArray.Count();
            for (int i = 0; i < size; i++)
            {
                serverConfigArray[i].Current += serverConfigArray[i].Weight;
                total += serverConfigArray[i].Weight;
                if (index == -1 || serverConfigArray[index].Current < serverConfigArray[i].Current)
                {
                    index = i;
                }
            }
            serverConfigArray[index].Current -= total;
            return index;
        }
    }
    public struct ServerConfig
    {
        //初始权重
        public int Weight { get; set; }

        //当前权重
        public int Current { get; set; }

        //服务名称
        public string Name { get; set; }
    }

}
