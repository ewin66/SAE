//using Microsoft.Web.Infrastructure.DynamicModuleHelper;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Web;
//using System.Web.Compilation;

//namespace SAE.CommonLibrary.Trace.Web
//{
//    public static class Initial
//    {
//        private readonly static object _lock = new object();
//        private static bool IsInit = false;
//        public static void Init()
//        {
//            if (!IsInit)
//            {
//                lock (_lock)
//                {
//                    if (!IsInit)
//                    {
//                        IsInit = true;
//                        DynamicModuleUtility.RegisterModule(typeof(TraceModule));
//                    }
//                }
//            }

//        }
//    }

//    internal class TraceModule : IHttpModule
//    {

//        public const string ServiceName = "ServiceName";

//        private string serviceName;

//        private ITraceProvider provider;

//        public TraceModule()
//        {
           
//        }

//        public void Dispose()
//        {
//            provider.Dispose();
//        }

//        public void Init(HttpApplication context)
//        {
//            //跟踪选项
//            var option = new TraceOptions();
//            //获得采样率
//            var rate = ConfigurationManager.AppSettings[$"Trace{nameof(option.SamplingRate)}"];
//            float tempRate;
//            if (rate != null && float.TryParse(rate, out tempRate))
//            {
//                option.SamplingRate = tempRate;
//            }
//            var endpoint = ConfigurationManager.AppSettings[$"Trace{nameof(option.Endpoint)}"];

//            if (!string.IsNullOrWhiteSpace(endpoint)) option.Endpoint = endpoint;

//            provider = TraceBuild.Create(option);

//            serviceName = ConfigurationManager.AppSettings[ServiceName] ?? BuildManager.GetGlobalAsaxType().BaseType.Assembly.GetName().Name;

//            //注册开始请求
//            context.BeginRequest += Context_BeginRequest;
//            //注册结束请求
//            context.EndRequest += Context_EndRequest;
//            //注册异常
//            context.Error += Context_Error;
           

//        }

//        private void Context_Error(object sender, EventArgs e)
//        {

//            var app = this.GetHttpApplication(sender);

//            var error = app.Server.GetLastError();

//            this.GetServerTrace(app)
//                   .Record("error", error?.Message ?? "An unknown error");
            
//        }

//        private void Context_EndRequest(object sender, EventArgs e)
//        {
//            this.GetServerTrace(this.GetHttpApplication(sender))
//                  .Dispose();
//        }
//        /// <summary>
//        /// 从httpcontext获得trace
//        /// </summary>
//        /// <param name="app"></param>
//        /// <returns></returns>
//        private ServerTrace GetServerTrace(HttpApplication app)
//        {

//            var serverTrace = app.Context.Items[ServiceName] as ServerTrace;

//            if (serverTrace == null) throw new NullReferenceException($"{serverTrace} is not {nameof(ServerTrace)} class");

//            return serverTrace;
//        }
//        /// <summary>
//        /// 获得HttpApplication
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <returns></returns>
//        private  HttpApplication GetHttpApplication(object sender)
//        {
//            var app = sender as HttpApplication;

//            if (app == null) throw new NullReferenceException($"{sender} is not {nameof(HttpApplication)} class");

//            return app;
//        }

//        private void Context_BeginRequest(object sender, EventArgs e)
//        {
//            var app = this.GetHttpApplication(sender);

//            var request = app.Context.Request;

//            var dic =new Dictionary<string, string>();

//            foreach(string key in request.Headers)
//            {
//                dic.Add(key, request.Headers[key]);
//            }

//            this.provider.Create(dic);

//            var server = new ServerTrace(serviceName,request.HttpMethod,request.Url.Port);

//            server.Record("http.host", request.Url.Host)
//                       .Record("http.uri", request.Url.AbsoluteUri)
//                       .Record("http.path", request.Url.AbsolutePath);
            
//            app.Context.Items[ServiceName] = server;
//        }

//    }
//}
