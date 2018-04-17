using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SAE.CommonLibrary.MQ
{
    /// <summary>
    /// MQ扩展程序
    /// </summary>
    public static class MQExtension
    {

        private static Func<Type, object> _provide;
        static MQExtension()
        {
            _provide = t => Activator.CreateInstance(t);
        }
        private static readonly ConcurrentDictionary<string, object> _store = new ConcurrentDictionary<string, object>();
        /// <summary>
        /// 异步发布
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="mq"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Task<IMQ> PublishAsync<TMessage>(this IMQ mq,TMessage message)
        {
            return Task.Run(() => mq.Publish(message));
        }

        /// <summary>
        /// 异步订阅
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="mq"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task<IMQ> SubscibeAsync<TMessage>(this IMQ mq,Action<TMessage> action)
        {
            return Task.Run(() => mq.Subscibe(action));
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TMessage">消息模型</typeparam>
        /// <param name="mq"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IMQ Subscibe<TMessage>(this IMQ mq,IHandler<TMessage> handler)
        {
            return mq.Subscibe<TMessage>(m => handler.Handle(m));
        }

        /// <summary>
        /// 异步订阅
        /// </summary>
        /// <typeparam name="TMessage">消息模型</typeparam>
        /// <param name="mq"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static Task<IMQ> SubscibeAsync<TMessage>(this IMQ mq,IHandler<TMessage> handler)
        {
            return Task.Run(() => mq.Subscibe(handler));
        }

        /// <summary>
        /// 订阅<typeparamref name="THandler"/>的所有<seealso cref="IHandler{TMessage}"/>接口的实现
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="mq"></param>
        /// <returns></returns>
        public static Task<IMQ> SubscibeTypeAsync<THandler>(this IMQ mq)where THandler:IHandler
        {
            var typeHandler = typeof(THandler);
            if (typeHandler.IsInterface)
            {
                return Task.FromResult(mq);
            }
            var handleInterface = typeof(IHandler<>);
            var subscibeMethod = typeof(MQExtension).GetMethod("SubscibeInternelHandle",BindingFlags.NonPublic|
                                                                                        BindingFlags.Static|
                                                                                        BindingFlags.IgnoreCase);
            var internalHandleType = typeof(Handle<>);
            Func<object> funcObject = () => _provide.Invoke(typeHandler);
            return Task.Run(() =>
            {
                foreach (var @interface in typeHandler.GetInterfaces())
                {
                    if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == handleInterface)
                    {
                        var argumentType = @interface.GenericTypeArguments[0];
                        var handleType = internalHandleType.MakeGenericType(argumentType);

                        var internalHandleObject = handleType.GetConstructors().First().Invoke(new object[] { funcObject });
                        subscibeMethod.MakeGenericMethod(argumentType)
                                      .Invoke(null, new object[] { mq,internalHandleObject});
                    }
                }
            }).ContinueWith(t =>
            {
                return mq;
            });
        }

        /// <summary>
        /// 订阅<typeparamref name="THandler"/>的所有<seealso cref="IHandler{TMessage}"/>接口的实现
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="mq"></param>
        /// <returns></returns>
        public static IMQ SubscibeType<THandler>(this IMQ mq) where THandler : IHandler
        {
            return mq.SubscibeTypeAsync<THandler>()
                     .GetAwaiter()
                     .GetResult();
        }

        /// <summary>
        /// 扫描该程序集所有实现了<seealso cref="IHandler{TMessage}"/>接口的类并订阅他们
        /// </summary>
        /// <param name="mq"></param>
        /// <param name="assemblys">程序集合</param>
        /// <returns></returns>
        public static Task<IMQ> SubscibeAssemblyAsync(this IMQ mq,params Assembly[] assemblys)
        {
            if (assemblys?.Count() <= 0)
            {
                assemblys = new Assembly[] { Assembly.GetCallingAssembly() };
            }

            var handleType = typeof(IHandler);

            var method = typeof(MQExtension).GetMethod("SubscibeType", new Type[] { typeof(IMQ) });

            return Task.Run(() =>
            {
                foreach (var assembly in assemblys)
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.IsClass && !type.IsInterface && handleType.IsAssignableFrom(type))
                        {
                            var methodInfo = method.MakeGenericMethod(type);
                            methodInfo.Invoke(null, new object[] { mq });
                        }
                    }
                }
            }).ContinueWith(t => mq);
            
        }

        /// <summary>
        /// 扫描该程序集所有实现了<seealso cref="IHandler{TMessage}"/>接口的类并订阅他们
        /// </summary>
        /// <param name="mq"></param>
        /// <param name="assemblys">程序集合</param>
        /// <returns></returns>
        public static IMQ SubscibeAssembly(this IMQ mq, params Assembly[] assemblys)
        {
            if (assemblys?.Count() <= 0)
            {
                assemblys = new Assembly[] { Assembly.GetCallingAssembly() };
            }
            return mq.SubscibeAssemblyAsync(assemblys)
                     .GetAwaiter()
                     .GetResult();
        }

        private static IMQ SubscibeInternelHandle<TMessage>(this IMQ mq,Handle<TMessage> handle)
        {            
            return mq.Subscibe<TMessage>(handle.Invoke); ;
        }

        /// <summary>
        /// 设置服务提供工厂函数
        /// </summary>
        /// <param name="mq"></param>
        /// <param name="serviceFactory"></param>
        /// <returns></returns>
        public static IMQ SetServiceFactory(this IMQ mq,Func<Type,object> serviceFactory)
        {
            _provide = serviceFactory;
            return mq;
        }

        /// <summary>
        /// 建立映射
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="mq"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IMQ Mapping<TMessage>(this IMQ mq, string key) where TMessage : class, new()
        {
            NameUtils.Add(typeof(TMessage), key);
            return mq;
        }
        
        /// <summary>
        /// 获得默认的编码
        /// </summary>
        /// <param name="mq"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(this IMQ mq)
        {
            return Encoding.UTF8;
        }

        
        /// <summary>
        /// 添加事件处理
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="mq"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static void Add<TMessage>(this IMQ mq, Action<TMessage> action)
        {
            Action<TMessage> actionMessage=null;

            var key = NameUtils.Get<TMessage>();

            object @object;
            lock (_store)
            {
                if (_store.TryGetValue(key, out @object))
                {
                    actionMessage = @object as Action<TMessage>;
                    actionMessage += action;
                }
                else
                {
                    actionMessage = action;
                }
                _store.AddOrUpdate(key, actionMessage, (a, b) => actionMessage);
            }
        }
        
        /// <summary>
        /// 获得处理函数
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="mq"></param>
        /// <returns></returns>
        public static Action<TMessage> Get<TMessage>(this IMQ mq)
        {
            Action<TMessage> actionMessage = null;

            var key = NameUtils.Get<TMessage>();
            object @object = null;
            if (_store.TryGetValue(key, out @object))
            {
                actionMessage = @object as Action<TMessage>;
            }

            if (actionMessage == null)
            {
                return m =>
                {
                    Log.LogHelper.Info<IMQ>($"订阅函数:\"{key}\"不存在");
                };
            }
            else
            {
                return actionMessage;
            }
        }

        /// <summary>
        /// 消息是否被订阅过,<see cref="bool.TrueString"/>订阅过，<see cref="bool.FalseString"/>尚未订阅
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="mq"></param>
        /// <returns></returns>
        public static bool IsExist<TMessage>(this IMQ mq)
        {
            var key = NameUtils.Get<TMessage>();
            return _store.ContainsKey(key);
        }
    }

    internal class Handle<T>
    {
        private readonly Func<object> _handler;
        public Handle(Func<object> handler)
        {
            this._handler = handler;
        }

        public void Invoke(T t)
        {
            (this._handler.Invoke() as IHandler<T>).Handle(t);
        }
    }
}
