#region 弃用的
//using NLog.Config;
//using NLog.Targets;
//using System;
//using System.IO;
//using System.Linq;
//using System.Net.Sockets;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Threading.Tasks;
//using NLog;

//namespace SAE.CommonLibrary.Log.Imp
//{
//    /// <summary>
//    /// LogStash基于Tcp的输入端
//    /// </summary>
//    [Target("LogStash")]
//    public sealed class LogStashTarget : TargetWithLayout
//    {

//        /// <summary>
//        /// 端口匹配模式
//        /// </summary>
//        private const string portPattern = ":([\\d]+)";
//        private string server;
//        private Timer _timer;
//        //300秒
//        private const int Interval = 300 * 1000;
//        private string failureToSavePath;
//        private const string Extension = ".json";
//        /// <summary>
//        /// 当发送的日志在3天以内都没有发送出去，那么我们视该条日志为无效的日志，将其放入Logs/Invalid中等待排查
//        /// </summary> 
//        private readonly string _invalidToSavePath = Path.Combine(
//#if NET45                                           
//                                                    AppDomain.CurrentDomain.BaseDirectory
//#else                                               
//                                                    AppContext.BaseDirectory
//#endif                                              
//                                                    , "Logs/Invalid");
//        /// <summary>
//        /// 重发超时,当日志产生时间超过了该限制则忽略该日志，并将其发送至Logs/Invalid文件中该值为3天;
//        /// </summary>
//        private const int ResendTimeout = 259200;

//        /// <summary>
//        /// 链接字符串
//        /// </summary>
//        [RequiredParameter]
//        public string ConnectionString
//        {
//            get
//            {
//                return server;
//            }
//            set
//            {
//                if (string.IsNullOrWhiteSpace(value))
//                {
//                    throw new ArgumentNullException(nameof(ConnectionString), "请输入有效的链接地址");
//                }
//                var defaultPort = 80;
//                var defaultServer = string.Empty;
//                var result = Regex.Match(value, portPattern);
//                if (result.Success)
//                {
//                    defaultServer = Regex.Replace(value, portPattern, "");
//                    defaultPort = int.Parse(result.Groups[1].Value);
//                }
//                else
//                {
//                    defaultServer = value;
//                }
//                port = defaultPort;
//                server = defaultServer;
//            }
//        }
//        /// <summary>
//        /// 消息重发机制，若为True，日志一旦发送失败，将会被记录至/{FailureToSavePath}目录下默认为True
//        /// </summary>
//        public bool Resend { get; set; }

//        /// <summary>
//        /// 失败保存到的路径默认是Logs/Failure
//        /// </summary>
//        public string FailureToSavePath
//        {
//            get
//            {
//                return failureToSavePath;
//            }
//            set
//            {
//                if (string.IsNullOrWhiteSpace(value)) return;
//                failureToSavePath = Path.Combine(
//#if NET45
//                                                    AppDomain.CurrentDomain.BaseDirectory
//#else
//                                                    AppContext.BaseDirectory
//#endif 
//                                                    , value);
//            }
//        }

//        /// <summary>
//        /// 端口
//        /// </summary>
//        private int port;
//        /// <summary>
//        /// 应用名称
//        /// </summary>
//        public string AppName { get; set; }

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public LogStashTarget()
//        {
//            FailureToSavePath = "Logs/Failure";
//            Resend = true;
//            _timer = new Timer(s =>
//            {
//                //检测文件是否存在
//                if (Directory.Exists(this.FailureToSavePath))
//                {
//                    //读取所有文件数据
//                    var jsonArray = Directory.GetFiles(this.FailureToSavePath, "*" + Extension).Select(fullPath =>
//                       {
//                          //读取文本所有数据
//                          var str = File.ReadAllText(fullPath);
//                          //移除文件
//                          File.Delete(fullPath);
//                           return str.Trim();
//                       });
//                    //重新发送
//                    foreach (var json in jsonArray)
//                    {
//                        this.Send(json);
//                    }
//                }
//                //定时器重启
//                _timer.Change(Interval, Timeout.Infinite);
//            }, null, 0, Timeout.Infinite);
//        }



//        /// <summary>
//        /// 重写日志
//        /// </summary>
//        /// <param name="logEvent"></param>
//        protected override void Write(LogEventInfo logEvent)
//        {
//            var message = this.RenderLogEvent(this.Layout, logEvent);
//            //发送数据
//            LogEvent @event = new LogEvent(AppName, logEvent.Level.Name, message);
//            this.Send(@event.ToString());
//            //base.Write(logEvent);
//        }
//        /// <summary>
//        /// 发送
//        /// </summary>
//        /// <param name="message"></param>
//        /// <returns></returns>
//        private Task Send(string message)
//        {
//            return Task.Run(async () =>
//            {
//                try
//                {
//                    using (var tcp = new TcpClient())
//                    {
//#if NET45
                  
//                        tcp.Connect(this.server, this.port);
//#else

//                        await tcp.ConnectAsync(this.server, this.port);
//#endif

//                        using (var stream = new StreamWriter(tcp.GetStream()))
//                        {
//                            await stream.WriteLineAsync(message);
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    try
//                    {
//                        var @event = Json.JsonHelper.Deserialize<LogEvent>(message);
//                        if (@event.LocalDate.AddSeconds(ResendTimeout) >= DateTime.Now)
//                        {
//                            this.WriteFailure($"{Guid.NewGuid()}{Extension}", message);
//                        }
//                        else
//                        {
//                            this.WriteInvalid($"{Guid.NewGuid()}{Extension}", message + Json.JsonHelper.Serialize(ex));
//                        }
//                    }
//                    catch (Exception e)
//                    {
//                        //此文件无效
//                        this.WriteInvalid($"{Guid.NewGuid()}.txt", message + Json.JsonHelper.Serialize(ex));
//                    }
//                }

//            });
//        }

//        private void WriteFailure(string path, string message)
//        {
//            if (!Directory.Exists(FailureToSavePath))
//            {
//                Directory.CreateDirectory(FailureToSavePath);
//            }
//            File.WriteAllText(Path.Combine(FailureToSavePath, path), message);
//        }
//        private void WriteInvalid(string path, string message)
//        {
//            if (!Directory.Exists(_invalidToSavePath))
//            {
//                Directory.CreateDirectory(_invalidToSavePath);
//            }
//            File.WriteAllText(Path.Combine(_invalidToSavePath, path), message);
//        }

//        /// <summary>
//        /// 释放定时器
//        /// </summary>
//        /// <param name="disposing"></param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                this._timer?.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
#endregion
