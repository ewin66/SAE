//using Autofac.Builder;
//using Autofac.Extras.DynamicProxy;
//using Castle.DynamicProxy;
//using SAE.CommonLibrary.Trace;
//using SAE.CommonLibrary.Trace.Web;
//using System;
//using System.Linq;

//namespace Autofac
//{
//    /// <summary>
//    /// autoface扩展
//    /// </summary>
//    public static class AutofacTraceExtension
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TLimit"></typeparam>
//        /// <typeparam name="TActivatorData"></typeparam>
//        /// <typeparam name="TRegistrationStyle"></typeparam>
//        /// <param name="serviceName">服务名称</param>
//        /// <param name="build">容器构造器，相同的serviceName只需构造一次</param>
//        /// <param name="containerBuilder"></param>
//        /// <returns></returns>
//        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> RegisterTrace<TLimit,  TActivatorData,TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> build, string serviceName="repository", ContainerBuilder containerBuilder=null)
//        {
            
//            containerBuilder?.Register(s => new AutofacTrace(serviceName))
//                             .Named<IInterceptor>(serviceName)
//                             .SingleInstance();

//            build.EnableInterfaceInterceptors()
//                 .InterceptedBy(serviceName);

//            return build;
//        }
//    }
    
//}
