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
        public static IServiceCollection AddQueryableHandle(this IServiceCollection services)
        {
            services.TryAddScoped(typeof(DefaultAddHandler<,>), typeof(DefaultAddHandler<,>));
            services.TryAddScoped(typeof(DefaultUpdateHandler<,>), typeof(DefaultUpdateHandler<,>));
            services.TryAddScoped(typeof(DefaultRemoveHandler<,>), typeof(DefaultRemoveHandler<,>));
            return services;
        }
        public static IRegistrationBuilder GetQueryableBuilder(this IMQ mq)
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
        /// 映射对象
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IRegistrationBuilder Mapping<TModel, TEvent>(this IRegistrationBuilder builder)where TModel:class where TEvent:class
        {
            return builder.Mapping(typeof(TModel), typeof(TEvent));
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
            var registration= builder as RegistrationBuilder;
            registration.Map(model, @event);
            return registration;
        }

        public static IMQ Build(this IRegistrationBuilder registrationBuilder)
        {
            var builder = registrationBuilder as RegistrationBuilder;

            builder.Build();

            return builder.MQ;
        }
    }
}
