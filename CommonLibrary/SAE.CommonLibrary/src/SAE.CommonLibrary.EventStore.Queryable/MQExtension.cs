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
        /// 添加查询处理器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddQueryableHandler(this IServiceCollection services)
        {
            services.TryAddScoped(typeof(DefaultAddHandler<,>), typeof(DefaultAddHandler<,>));
            services.TryAddScoped(typeof(DefaultUpdateHandler<,>), typeof(DefaultUpdateHandler<,>));
            services.TryAddScoped(typeof(DefaultRemoveHandler<,>), typeof(DefaultRemoveHandler<,>));
            return services;
        }
        /// <summary>
        /// 创建查询构建者
        /// </summary>
        /// <param name="mq"></param>
        /// <returns></returns>
        public static IRegistrationBuilder CreateQueryableBuilder(this IMQ mq)
        {
            return new RegistrationBuilder(mq);
        }

        public static IRegistrationBuilder RegisterAssembly(this IRegistrationBuilder registrationBuilder, params Assembly[] assemblies)
        {
            if (assemblies == null || !assemblies.Any())
                assemblies = new Assembly[1] { Assembly.GetCallingAssembly() };
            (registrationBuilder as RegistrationBuilder).Add(assemblies);
            return registrationBuilder;
        }

   
        public static IRegistrationBuilder RegisterType(this IRegistrationBuilder registrationBuilder, params Type[] types)
        {
            (registrationBuilder as RegistrationBuilder).Add(types);
            return registrationBuilder;
        }

        public static IRegistrationBuilder Register<TEvent>(this IRegistrationBuilder registrationBuilder) where TEvent : IEvent
        {
            var registration = registrationBuilder as RegistrationBuilder;
            return registration.RegisterType(typeof(TEvent));
        }
       

        public static IRegistrationBuilder Filter(this IRegistrationBuilder registrationBuilder,Func<Type,bool> filter)
        {
            var builder = registrationBuilder as RegistrationBuilder;
            builder._store.RemoveAll(t => !filter.Invoke(t));
            return registrationBuilder;
        }

        /// <summary>
        /// 搜索<typeparamref name="TEvent"/>的<seealso cref="ModelAttribute"/>特性，
        /// 或使用命名约束的方式映射一个默认的处理对象
        /// </summary>
        /// <typeparam name="TModel">具体类型</typeparam>
        /// <typeparam name="TEvent">事件对象</typeparam>
        /// <param name="builder">构建接口</param>
        /// <returns></returns>
        public static IRegistrationBuilder Mapping<TModel, TEvent>(this IRegistrationBuilder builder)where TModel:class where TEvent:class
        {
            return builder.Mapping<TModel, TEvent>(HandlerEnum.None);
        }

        public static IRegistrationBuilder Mapping<TModel, TEvent>(this IRegistrationBuilder builder,HandlerEnum handler) where TModel : class where TEvent : class
        {
            return builder.Mapping(typeof(TModel), typeof(TEvent), handler);
        }

        /// <summary>
        /// 映射对象
        /// </summary>
        /// <param name="builde"></param>
        /// <param name="model"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        public static IRegistrationBuilder Mapping(this IRegistrationBuilder builder,Type model,Type @event)
        {
       
            return builder.Mapping(model,@event,HandlerEnum.None);
        }

        public static IRegistrationBuilder Mapping(this IRegistrationBuilder builder, Type model, Type @event,HandlerEnum handler)
        {
            var registration = builder as RegistrationBuilder;
            registration.Map(model, @event, handler);
            return registration;
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="registrationBuilder"></param>
        /// <returns></returns>
        public static IMQ Build(this IRegistrationBuilder registrationBuilder)
        {
            var builder = registrationBuilder as RegistrationBuilder;

            builder.Build();

            return builder.MQ;
        }
    }
}
