using SAE.CommonLibrary.MQ;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using SAE.CommonLibrary.EventStore.Queryable.Handle;

namespace SAE.CommonLibrary.EventStore.Queryable.Builder
{
    /// <summary>
    /// 处理类型
    /// </summary>
    public enum HandlerEnum : int
    {
        /// <summary>
        /// 空的
        /// </summary>
        None = -1,
        /// <summary>
        /// 添加
        /// </summary>
        Add = 0,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 1,
        /// <summary>
        /// 移除
        /// </summary>
        Remove = 2
    }

    /// <summary>
    /// 映射对象
    /// </summary>
    public class Map
    {
        /// <summary>
        /// 创建一个完整的Map对象
        /// </summary>
        /// <param name="modelType">模型类型</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="handle">处理方式</param>
        public Map(Type modelType, Type eventType, HandlerEnum handle)
        {
            this.ModelType = modelType;
            this.EventType = eventType;
            this.Handle = handle;
        }
        /// <summary>
        /// 创建一个拥有<seealso cref="Map.EventType"/>和<seealso cref="Map.Handle"/>的Map
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handle">处理方式</param>
        public Map(Type eventType, HandlerEnum handle) : this(eventType)
        {
            this.Handle = handle;
        }
        /// <summary>
        /// 创建一个拥有<seealso cref="Map.EventType"/>和<seealso cref="HandlerEnum.None"/>的Map
        /// </summary>
        /// <param name="eventType">事件类型</param>
        public Map(Type eventType)
        {
            this.Handle = HandlerEnum.None;
            
            var modelType = typeof(ModelAttribute);

            var attribute = eventType.GetCustomAttributes<ModelAttribute>().FirstOrDefault();
            //扫描ModelAttribute类型
            if (attribute != null)
            {
                this.ModelType = attribute.Type;
                this.Handle = attribute.Handle;
            }
            this.EventType = eventType;
        }
        /// <summary>
        /// 模型类型
        /// </summary>
        public Type ModelType { get; internal set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public Type EventType { get; internal set; }
        /// <summary>
        /// 处理方式
        /// </summary>
        public HandlerEnum Handle { get; internal set; }

        /// <summary>
        /// 获得处理程序
        /// </summary>
        /// <returns></returns>
        public Type GetHandle()
        {

            Type handleType = null;
            if (this.ModelType == null) return handleType;

            if (this.Handle == HandlerEnum.None)
            {
                this.NamingConventionsScan();
            }

            switch (this.Handle)
            {
                case HandlerEnum.Add:
                    {
                        handleType = typeof(DefaultAddHandler<,>);
                        break;
                    }
                case HandlerEnum.Update:
                    {
                        handleType = typeof(DefaultUpdateHandler<,>);
                        break;
                    }
                case HandlerEnum.Remove:
                    {
                        handleType = typeof(DefaultRemoveHandler<,>);
                        break;
                    }
                default: return handleType;
            }

            return handleType.MakeGenericType(this.ModelType, this.EventType);
        }

        /// <summary>
        /// 以命名约定扫描
        /// </summary>
        private void NamingConventionsScan()
        {
            var type = this.EventType;

            if (type.Name.EndsWith("CreateEvent"))
            {
                this.Handle = HandlerEnum.Add;
            }

            if (type.Name.EndsWith($"{nameof(HandlerEnum.Remove)}Event"))
            {
                this.Handle = HandlerEnum.Remove;
            }

            if (type.Name.EndsWith($"{nameof(HandlerEnum.Update)}Event"))
            {
                this.Handle = HandlerEnum.Update;
            }
        }

        #region override object
        public override bool Equals(object obj)
        {
            return this == (Map)obj;
        }

        public static bool operator ==(Map left, Map right)
        {
            if ((object)left == null) return (object)right==null;

            if ((object)right == null) return false;

            if (ReferenceEquals(left, right)) return true;

            return left.GetHashCode() == right.GetHashCode() && left.ToString() == right.ToString();
        }
        public static bool operator !=(Map left, Map right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Handle}_{(this.ModelType?.ToString() ?? string.Empty)}_{(this.EventType?.ToString() ?? string.Empty)}";
        } 
        #endregion
    }


    /// <summary>
    /// 构建者具体的实现
    /// </summary>
    internal class RegistrationBuilder : IRegistrationBuilder
    {
        /// <summary>
        /// 创建一个构建者对象
        /// </summary>
        /// <param name="mq"></param>
        internal RegistrationBuilder(IMQ mq)
        {
            this.MQ = mq;
            this._store = new List<Map>();
        }
        
        private IMQ MQ { get; }

        private readonly List<Map> _store;

        public IEnumerable<Map> Maps { get => this._store; }

        public void Add(Map map)
        {
            if (this.Maps.Any(t => t == map))
            {
                var mp = this.Maps.FirstOrDefault(t => t == map);
                mp.Handle = map.Handle;
            }
            else
            {
                this._store.Add(map);
            }
        }

        public void Build()
        {
            foreach (var map in this.Maps)
            {
                var handle = map.GetHandle();
                if (handle != null)
                {
                    //订阅事件
                    this.MQ.SubscibeType(handle);
                }
            }
        }


    }
}
