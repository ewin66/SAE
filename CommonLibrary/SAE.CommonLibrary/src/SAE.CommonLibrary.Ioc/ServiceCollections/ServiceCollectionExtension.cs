using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SAE.CommonLibrary.Ioc.ServiceCollections
{
    /// <summary>
    /// <see cref="IServiceCollection"/>扩展方法
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 注册SAE.CommonLibrary企业组件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRelative(this IServiceCollection services, ServiceLifetime lifetime=ServiceLifetime.Transient) => services.AddRelative("SAE.CommonLibrary", lifetime);
        /// <summary>
        /// 注册相对程序集
        /// </summary>
        /// <param name="services"></param>
        /// <param name="relativeName"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddRelative(this IServiceCollection services, string relativeName, ServiceLifetime lifetime)
        {

            foreach (var filePath in Directory.GetFiles(AppContext.BaseDirectory, $"{relativeName}.*.dll"))
            {
                Assembly.LoadFrom(filePath);
            }

            var assemblies = AppDomain.CurrentDomain
                                      .GetAssemblies()
                                      .ToList()
                                      .Where(s => s.GetName().Name.StartsWith(relativeName, StringComparison.CurrentCultureIgnoreCase))
                                      .ToArray();

            return services.AddAssemblyType(lifetime,assemblies);
        }
        /// <summary>
        /// 扫描程序集并完成自动注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="lifetime">生命周期</param>
        /// <param name="assemblies">程序集合</param>
        /// <returns></returns>
        public static IServiceCollection AddAssemblyType(this IServiceCollection services, ServiceLifetime lifetime, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Count() <= 0) return services;

            var types = assemblies.SelectMany(s => s.GetTypes()
                                                    .Where(t => t.IsPublic && ((t.IsClass && !t.IsAbstract) || t.IsInterface)));
            var classTypes = types.Where(s => s.IsClass);

            foreach (var interfaceType in types.Where(t => t.IsInterface))
            {
                Type impType;
                if (!interfaceType.IsGenericTypeDefinition)
                {
                    impType = classTypes.FirstOrDefault(t => !t.IsGenericTypeDefinition && interfaceType.IsAssignableFrom(t));
                }
                else
                {
                    var typeArgs = new List<Type>();
                    foreach (var type in interfaceType.GetGenericArguments())
                    {
                        typeArgs.Add(type);
                    }

                    var makeType = interfaceType.MakeGenericType(typeArgs.ToArray());
                    impType = classTypes.FirstOrDefault(t =>
                      {
                          var impArgs = t.GetGenericArguments();
                          if (t.IsGenericTypeDefinition && impArgs.Length == typeArgs.Count)
                          {
                              for (var i = 0; i < impArgs.Length; i++)
                              {
                                  if (impArgs[i].BaseType != typeArgs[i].BaseType)
                                  {
                                      return false;
                                  }    
                              }
                              var impMakeType = t.MakeGenericType(typeArgs.ToArray());
                              return interfaceType.IsAssignableFrom(impMakeType);
                          }
                          return false;
                      });
                }

                if (impType != null)
                    services.TryAdd(new ServiceDescriptor(interfaceType, impType, lifetime));
            }
            

            return services;
        }
    }
}
