using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.Cache.Redis
{
    /// <summary>
    /// redis配置
    /// </summary>
    public class RedisConfig
    {
        public RedisConfig()
        {
            _services = new List<string>();
        }
        /// <summary>
        /// DB库
        /// </summary>
        public int DB { get; set; }
        string connection;
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string Connection
        {
            get
            {
                return connection;
            }
            set
            {
                this.connection = value;
                foreach(Match match in Regex.Matches(value, ServicePattern))
                {
                    this._services.Add(match.Value);
                }
            }
        }

        internal readonly List<string> _services;

        const string ServicePattern = @"((\w*\.){1,3}\w*:\d{1,5})";

        internal string ElectServer()
        {
            var obj = new object();

            return _services[obj.GetHashCode() % _services.Count];
        }
    }
}
