using SAE.CommonLibrary.EventStore.Queryable.Builder;
using SAE.CommonLibrary.MQ;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.EventStore.Queryable.Handle;

namespace SAE.CommonLibrary.EventStore.Queryable
{
    /// <summary>
    /// MQ扩展
    /// </summary>
    public static class MQExtension
    {

        /// <summary>
        /// 创建查询构建者
        /// </summary>
        /// <param name="mq"></param>
        /// <returns></returns>
        public static IRegistrationBuilder CreateBuilder(this IMQ mq)
        {
            return new RegistrationBuilder(mq);
        }

        /// <summary>
        /// 扫描程序集
        /// </summary>
        /// <param name="registrationBuilder"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IRegistrationBuilder RegisterAssembly(this IRegistrationBuilder registrationBuilder, params Assembly[] assemblies)
        {
            if (assemblies == null || !assemblies.Any())
                assemblies = new Assembly[1] { Assembly.GetCallingAssembly() };

            foreach (var assembly in assemblies)
                registrationBuilder.RegisterType(assembly.GetTypes());

            return registrationBuilder;
        }

        /// <summary>
        /// 扫描类型
        /// </summary>
        /// <param name="registrationBuilder"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IRegistrationBuilder RegisterType(this IRegistrationBuilder registrationBuilder, params Type[] types)
        {
            if (types != null && types.Any())
            {
                var eventType = typeof(IEvent);

                var typesList = types.Where(t => !t.IsInterface &&
                                                 !t.IsAbstract &&
                                                 t.IsClass &&
                                                 eventType.IsAssignableFrom(t));
                foreach (var type in typesList)
                {
                    registrationBuilder.Add(new Map(type));
                }
            }
            return registrationBuilder;
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="registrationBuilder"></param>
        /// <returns></returns>
        public static IRegistrationBuilder Register<TEvent>(this IRegistrationBuilder registrationBuilder) where TEvent : IEvent
        {
            var registration = registrationBuilder as RegistrationBuilder;
            return registration.RegisterType(typeof(TEvent));
        }

        /// <summary>
        /// 搜索<typeparamref name="TEvent"/>的<seealso cref="ModelAttribute"/>特性，
        /// 或使用命名约束的方式映射一个默认的处理对象
        /// </summary>
        /// <typeparam name="TModel">具体类型</typeparam>
        /// <typeparam name="TEvent">事件对象</typeparam>
        /// <param name="builder">构建接口</param>
        /// <returns></returns>
        public static IRegistrationBuilder Mapping<TModel, TEvent>(this IRegistrationBuilder builder) where TModel : class where TEvent : IEvent
        {
            return builder.Mapping<TModel, TEvent>(HandlerEnum.None);
        }

        /// <summary>
        /// 映射<typeparamref name="TModel"/>和<typeparamref name="TEvent"/>并以<paramref name="handler"/>方式进行处理
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="builder"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IRegistrationBuilder Mapping<TModel, TEvent>(this IRegistrationBuilder builder, HandlerEnum handler) where TModel : class where TEvent : IEvent
        {
            builder.Mapping(typeof(TModel), typeof(TEvent), handler);
            return builder;
        }

        /// <summary>
        /// 映射<typeparamref name="TModel"/>和<typeparamref name="TEvent"/>并以<seealso cref="HandlerEnum.None"/>方式进行处理
        /// </summary>
        /// <param name="builde"></param>
        /// <param name="model"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        public static IRegistrationBuilder Mapping(this IRegistrationBuilder builder, Type model, Type @event)
        {
            builder.Mapping(model, @event, HandlerEnum.None);
            return builder;
        }

        /// <summary>
        /// 指定<paramref name="model"/>和<paramref name="event"/>以<paramref name="handler"/>的方式映射处理
        /// </summary>
        /// <param name="builder">注册构建者</param>
        /// <param name="model">模型类型</param>
        /// <param name="event">事件类型</param>
        /// <param name="handler">处理方式</param>
        /// <returns>返回注册构建者</returns>
        public static IRegistrationBuilder Mapping(this IRegistrationBuilder builder, Type model, Type @event, HandlerEnum handler)
        {
            var registration = builder as RegistrationBuilder;
            registration.Add(new Map(model, @event, handler));
            return registration;
        }
        /// <summary>
        /// 指定接下来的映射是以<typeparamref name="TModel"/>为ModelType的
        /// </summary>
        /// <typeparam name="TModel">模型类型</typeparam>
        /// <param name="registrationBuilder">注册构建者</param>
        /// <returns>返回一个受限的构建者</returns>
        public static ILimitRegistrationBuilder Mapping<TModel>(this IRegistrationBuilder registrationBuilder) where TModel : class
        {
            return new LimitRegistrationBuilder<TModel>(registrationBuilder);
        }
        /// <summary>
        /// 指定接下来的映射是以<typeparamref name="TModel"/>为ModelType的
        /// </summary>
        /// <typeparam name="TModel">模型类型</typeparam>
        /// <param name="limitRegistrationBuilder">受限的构建者</param>
        /// <returns>返回一个受限的构建者</returns>
        public static ILimitRegistrationBuilder Mapping<TModel>(this ILimitRegistrationBuilder limitRegistrationBuilder) where TModel : class
        {
            return new LimitRegistrationBuilder<TModel>(limitRegistrationBuilder.RegistrationBuilder);
        }
        /// <summary>
        /// 将<seealso cref="ILimitRegistrationBuilder.RegistrationBuilder"/>中所有未被定义处理方式和ModelType的的都指定为<paramref name="handler"/>
        /// </summary>
        /// <param name="limitRegistration">受限的构建者</param>
        /// <param name="handler">处理方式</param>
        /// <returns>返回一个受限的构建者</returns>
        public static ILimitRegistrationBuilder Mapping(this ILimitRegistrationBuilder limitRegistration, HandlerEnum handler)
        {
            return limitRegistration.Mapping(handler, null);
        }

        /// <summary>
        /// 将<seealso cref="ILimitRegistrationBuilder.RegistrationBuilder"/>中所有未被定义处理方式和ModelType的并
        /// 使用<paramref name="predicate"/>筛选后的类型都指定为<paramref name="handler"/>
        /// </summary>
        /// <param name="limitRegistration">受限的构建者</param>
        /// <param name="handler">处理方式</param>
        /// <returns>返回一个受限的构建者</returns>
        public static ILimitRegistrationBuilder Mapping(this ILimitRegistrationBuilder limitRegistration, HandlerEnum handler, Func<Type, bool> predicate)
        {
            var maps = limitRegistration.RegistrationBuilder
                                        .Maps
                                        .Where(s => s.ModelType == limitRegistration.ModelType &&
                                               s.Handle == HandlerEnum.None);
            List<Map> mapList = new List<Map>();

            if (predicate == null)
            {
                mapList.AddRange(maps);
            }
            else
            {
                mapList.AddRange(maps.Where(s => predicate.Invoke(s.EventType)));
            }

            mapList.ForEach(map =>
            {
                map.Handle = handler;
                map.ModelType = limitRegistration.ModelType;
            });

            return limitRegistration;
        }
        /// <summary>
        /// 自动扫描<seealso cref="ILimitRegistrationBuilder.RegistrationBuilder"/>中没有设置ModelType且
        /// 不为<seealso cref="HandlerEnum.None"/>的ModelType都指定为<seealso cref="ILimitRegistrationBuilder.ModelType"/>
        /// </summary>
        /// <param name="limitRegistration">受限的构建者</param>
        /// <returns>返回一个受限的构建者</returns>
        public static ILimitRegistrationBuilder Mapping(this ILimitRegistrationBuilder limitRegistration)
        {
            return limitRegistration.Mapping(predicate:null);
        }
        /// <summary>
        /// 自动扫描<seealso cref="ILimitRegistrationBuilder.RegistrationBuilder"/>中没有设置ModelType且
        /// 不为<seealso cref="HandlerEnum.None"/>的ModelType都指定为<seealso cref="ILimitRegistrationBuilder.ModelType"/>
        /// </summary>
        /// <param name="limitRegistration">受限的构建者</param>
        /// <param name="predicate">筛选方式</param>
        /// <returns>返回一个受限的构建者</returns>
        public static ILimitRegistrationBuilder Mapping(this ILimitRegistrationBuilder limitRegistration, Func<Type, bool> predicate)
        {
            var maps = limitRegistration.RegistrationBuilder
                                        .Maps
                                        .Where(s => s.ModelType == null &&
                                               s.Handle == HandlerEnum.None);

            List<Map> mapList = new List<Map>();

            if (predicate == null)
            {
                mapList.AddRange(maps);
            }
            else
            {
                mapList.AddRange(maps.Where(s => predicate.Invoke(s.EventType)));
            }

            mapList.ForEach(map => map.ModelType = limitRegistration.ModelType);

            return limitRegistration;
        }

    }
}
