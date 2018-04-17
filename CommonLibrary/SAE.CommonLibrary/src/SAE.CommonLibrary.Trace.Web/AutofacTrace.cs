//using Castle.DynamicProxy;
//using System;
//using System.Linq;

//namespace SAE.CommonLibrary.Trace.Web
//{
//    /// <summary>
//    /// 依赖注入跟踪
//    /// </summary>
//    public class AutofacTrace : IInterceptor
//    {
//        /// <summary>
//        /// 拦截事件
//        /// </summary>
//        public static event Action<IInvocation> InvocationEvent;
//        /// <summary>
//        /// 服务名称
//        /// </summary>
//        private readonly string _serviceName;
//        /// <summary>
//        /// 服务名称
//        /// </summary>
//        /// <param name="serviceName"></param>
//        public AutofacTrace(string serviceName)
//        {
//            this._serviceName = serviceName;
//        }
//        /// <summary>
//        /// 拦截
//        /// </summary>
//        /// <param name="invocation"></param>
//        public void Intercept(IInvocation invocation)
//        {
//            InvocationEvent?.Invoke(invocation);
//            using (var localTrace = new LocalTrace(_serviceName, "call"))
//            {
//                localTrace.Record(nameof(invocation.Method), invocation.Method.Name)
//                          .Record(nameof(invocation.Arguments),
//                                  invocation.Arguments.Count().ToString())
//                          .Record(nameof(invocation.TargetType),
//                                  invocation.TargetType.Name);
//                try
//                {
//                    invocation.Proceed();
//                }
//                catch (Exception ex)
//                {
//                    localTrace.Record("error", ex.Message);
//                    throw ex;
//                }
//            }
//        }
//    }
//}
