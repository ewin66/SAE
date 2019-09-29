using zipkin4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zipkin = zipkin4net;
using System.Linq;

namespace SAE.CommonLibrary.Trace
{
    /// <summary>
    /// 跟踪基类
    /// </summary>
    public abstract class BaseTrace:IDisposable
    {
        private const string Exception = "error";
        /// <summary>
        /// 
        /// </summary>
        public BaseTrace()
        {
            this._beginDate = DateTime.Now;
        }
        /// <summary>
        /// 
        /// </summary>
        protected zipkin.Trace Trace
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        protected readonly DateTime _beginDate;

        /// <summary>
        /// 记录事件
        /// </summary>
        protected event Action RecordEvent;
        /// <summary>
        /// 记录消息
        /// </summary>
        /// <param name="key">tag</param>
        /// <param name="value">正文</param>
        public BaseTrace Record(string key, string value)
        {
            if (!this.IsSampled)
            {
                if (Unity.ImposedPublish(key))
                {
                    this.ForceSampled();
                }
               
            }

            var dateTime=DateTime.Now;

            this.RecordEvent += () => this.Trace.Record(Annotations.Tag(key, value), dateTime);

            return this;
        }

        /// <summary>
        /// 强制记录
        /// </summary>
        protected virtual void ForceSampled()
        {
            if (!this.IsSampled)
            {
                this.Trace.ForceSampled();
            }
        }

        /// <summary>
        /// 采样
        /// </summary>
        protected bool IsSampled
        {
            get => this.Trace.CurrentSpan.Sampled.HasValue ? this.Trace.CurrentSpan.Sampled.Value : false;
        }

        /// <summary>
        /// 执行记录事件
        /// </summary>
        protected void OnRecord()
        {
            this.RecordEvent.Invoke();
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            this.Record(BaseTrace.Exception, ex == null ? "unknown error" : ex.Message);
        }

        /// <summary>
        /// 释放所占资源，在每次走出服务所调用的区域时应当手动释放资源。
        /// </summary>
        public virtual void Dispose()
        {
            var timeSpan = DateTime.Now - this._beginDate;
            if (this.IsSampled || timeSpan >= TimeSpan.FromSeconds(1.0))
            {
                this.ForceSampled();
                this.OnRecord();
            }

        }
        /// <summary>
        /// 跟踪方法
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public virtual async Task TracedActionAsync(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                this.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// 跟踪方法
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public virtual async Task<T> TracedActionAsync<T>(Task<T> task)
        {
            T result;
            try
            {
                result = await task;
            }
            catch (Exception ex)
            {
                this.Error(ex);
                throw;
            }
            return result;
        }
    }
}
