using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.Log;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using SAE.CommonLibrary.Common;

namespace SAE.CommonLibrary.MQ.RabbitMQ
{
    /// <summary>
    /// MQ，RabbitMQ的实现类
    /// </summary>
    public class MQ : IMQ
    {
        #region Private Readonly Field
        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object _lock = new object();
        /// <summary>
        /// mq配置
        /// </summary>
        private readonly MQConfig _config;
        /// <summary>
        /// 链接
        /// </summary>
        private readonly IConnection _conn;
        /// <summary>
        /// 信道集合
        /// </summary>
        private readonly Dictionary<string, IModel> _channels;
        /// <summary>
        /// json转换器
        /// </summary>
        private readonly IJsonConvertor _jsonConvertor;
        /// <summary>
        /// log日志
        /// </summary>
        private readonly ILog _log;
        
        #endregion

        #region Const
        /// <summary>
        /// 
        /// </summary>
        private const string MQType = ExchangeType.Fanout;
        #endregion

        #region Ctor
        ~MQ()
        {
            this.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mqConfig"></param>
        /// <param name="jsonConvertor"></param>
        /// <param name="log"></param>
        public MQ(MQConfig mqConfig, IJsonConvertor jsonConvertor,ILog<MQ> log)
        {
            this._log = log;

            _config = mqConfig;

            _jsonConvertor = jsonConvertor;

            var factory = new ConnectionFactory
            {
                Uri = new Uri(this._config.Url),
                AutomaticRecoveryEnabled = _config.AutoReset
            };

            _conn = factory.CreateConnection();
            
            _conn.CallbackException += _conn_CallbackException;

            _channels = new Dictionary<string, IModel>();
        }

        /// <summary>
        /// 连接异常回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _conn_CallbackException(object sender, CallbackExceptionEventArgs e)
        {
            //连接异常
            this._log.Error("Callback Exception", e.Exception);
        }
        #endregion

        #region IMQ Member
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public IMQ Publish<TMessage>(TMessage message)
        {
            this._log.Debug("Start Publish");
            //获得exchang名称
            var name = NameUtils.Get(message);
            this._log.Debug($"Exchange Name:{name}");

            if (!_channels.ContainsKey(name))
            {
                lock (_lock)
                {
                    if (!_channels.ContainsKey(name))
                    {
                        this.AddChannel<TMessage>(name);
                    }
                }
            }
            //获得channel
            var channel = _channels[name];

            var propertie = channel.CreateBasicProperties();
            //设置消息为持久化
            propertie.DeliveryMode = 2;
            
            var json = this._jsonConvertor.Serialize(message);

            //序列主体
            var body = this.GetEncoding().GetBytes(json);
            try
            {
                this._log.Debug($"Publish Message:{json}");
                channel.BasicPublish(name, "", propertie, body);
            }
            catch(Exception ex)
            {
                this._log.Error($"body:{json},error:{this._jsonConvertor.Serialize(ex)}");
            }
            //发布

            this._log.Debug($"Publish Succeed:{json}");
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public IMQ Subscibe<TMessage>(Action<TMessage> action)
        {
            if (this.IsExist<TMessage>())
            {
                //已被订阅过只需要将订阅附加即可不需要再次开启通道
                this.Add(action);
                return this;
            }

            this.Add(action);

            var name = NameUtils.Get<TMessage>();
            this._log.Info($"Exchange Name:{name}");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "监听的队列不存在");

            var queryName = $"{this._config.Query}_{name}";
            this._log.Info($"Query Name:{queryName}");
            //创建信道
            var channel = _conn.CreateModel();

            channel.CallbackException += _conn_CallbackException;
            this._log.Debug($"Declare Queue:{queryName}");
            //定义队列
            channel.QueueDeclare(queryName, true, false, false, null);
            this._log.Debug($"Declare Exchange:{name}");
            //定义exchange
            channel.ExchangeDeclare(name, MQType, true, false);
            this._log.Debug($"Bind Queue:exchange={name},queue={queryName}");
            //绑定队列
            channel.QueueBind(queryName, name, "", null);

            this._log.Debug("Create Consumer");
            //创建消费者
            var consumer = new EventingBasicConsumer(channel);

            this._log.Debug("Create Received");
            
            //消费
            consumer.Received += (mo, arg) =>
            {
                var body = this.GetEncoding().GetString(arg.Body);

                var message = this._jsonConvertor.Deserialize<TMessage>(body);

                this._log.Debug($"Deserialize");

                try
                {
                    this._log.Debug($"Start Received:{message}");

                    this.Get<TMessage>().Invoke(message);

                    if (this._config.Ack)
                    {
                        this._log.Info("Send Ack");

                        channel.BasicAck(arg.DeliveryTag, false);
                    }   
                }
                catch(SAEException ex)
                {
                    channel.BasicReject(arg.DeliveryTag, true);
                    //消息消费失败
                    this._log.Error($"The exception caused by the consumption Message", ex);
                }
                catch(Exception ex)
                { 
                    //消息消费失败
                    this._log.Error($"The exception caused by the consumption Message",ex);
                }
                
            };
            
            if (this._config.Ack)
            {
                this._log.Info("Enable Ack");
                channel.BasicQos(0, this._config.Number, false);
            }
                

            //基础消费者
            channel.BasicConsume(queryName, !this._config.Ack, consumer);

            return this;
        }
        #endregion

        #region Private Method
        private void AddChannel<TMessage>(string name)
        {
            var channel = _conn.CreateModel();

            this._log.Info($"Create Exchange:{name}");

            channel.ExchangeDeclare(name,
                                    MQType,
                                    true,
                                    false,
                                    null);

            channel.CallbackException += _conn_CallbackException;

            _channels[name] = channel;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
        
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

            if (!disposedValue)
            {
                this._log?.Info("Start Dispose");
                this._conn?.Dispose();
                disposedValue = true;
            }

            GC.SuppressFinalize(this);
        }
        #endregion



        #endregion


    }
}
