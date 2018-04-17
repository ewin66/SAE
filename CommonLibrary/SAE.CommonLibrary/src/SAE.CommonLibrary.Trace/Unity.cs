using System.Collections.Generic;
using System.Linq;
namespace SAE.CommonLibrary.Trace
{
    internal class Unity
    {
        public static readonly IEnumerable<string> PublishItems = new List<string>
        {
            "error",
        };

        /// <summary>
        /// 如果返回true则trace需要强制发布
        /// </summary>
        /// <param name="tag">要验证的tag</param>
        /// <returns></returns>
        public static bool ImposedPublish(string tag)
        {
            return PublishItems.Any(s => s.ToLower() == tag.ToLower());
        }
        /// <summary>
        /// 获得跟踪对象
        /// </summary>
        /// <returns></returns>
        public static zipkin4net.Trace Get()
        {
            if (zipkin4net.Trace.Current == null)
            {
                var current = zipkin4net.Trace.Create();
                //current.CurrentSpan.SamplingStatus = zipkin4net.SamplingStatus.NoDecision;
                zipkin4net.Trace.Current = current;
            }
                
            
            return zipkin4net.Trace.Current;
        }
        /// <summary>
        /// 设置当前跟踪对象
        /// </summary>
        /// <param name="trace"></param>
        public static void Set(zipkin4net.Trace trace)
        {
            zipkin4net.Trace.Current = trace;
        }

    }
}
