using SAE.CommonLibrary.MQ;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using SAE.CommonLibrary.EventStore.Queryable.Handle;

namespace SAE.CommonLibrary.EventStore.Queryable.Builder
{
    public enum HandlerEnum : int
    {
        /// <summary>
        /// 空的
        /// </summary>
        None = -1,
        /// <summary>
        /// 添加
        /// </summary>
        Add,
        /// <summary>
        /// 更新
        /// </summary>
        Update,
        /// <summary>
        /// 移除
        /// </summary>
        Remove
    }

    internal class Map
    {
        public Map(Type type, HandlerEnum handle)
        {
            this.Type = type;
            this.Handle = handle;
        }
        public Type Type { get; }
        public HandlerEnum Handle { get; set; }

        public Type GetHandle()
        {
            Type handleType = null;

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
            }

            return handleType;
        }

        /// <summary>
        /// 以命名约定扫描
        /// </summary>
        private void NamingConventionsScan()
        {
            var type = this.Type;

            if (type.Name.StartsWith(nameof(HandlerEnum.Add)))
            {
                this.Handle = HandlerEnum.Add;
            }

            if (type.Name.StartsWith(nameof(HandlerEnum.Remove)))
            {
                this.Handle = HandlerEnum.Remove;
            }

            if (type.Name.StartsWith(nameof(HandlerEnum.Update)))
            {
                this.Handle = HandlerEnum.Update;
            }
        }
    }

   
    /// <summary>
    /// 构建者具体的实现
    /// </summary>
    internal class RegistrationBuilder : IRegistrationBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mq"></param>
        internal RegistrationBuilder(IMQ mq)
        {
            this.MQ = mq;
            this._store = new List<Type>();
            this._maps = new Dictionary<Type, IList<Map>>();
        }

        public RegistrationBuilder(IMQ mq, params Assembly[] assemblies) : this(mq)
        {
            this.Add(assemblies);
        }

        public readonly List<Type> _store;

        internal IMQ MQ { get; }

        private readonly IDictionary<Type, IList<Map>> _maps;

        public RegistrationBuilder(IMQ mq, params Type[] types) : this(mq)
        {
            this.Add(types);
        }

        /// <summary>
        /// 添加事件类型
        /// </summary>
        /// <param name="types"></param>
        public void Add(params Type[] types)
        {
            if (types == null || !types.Any()) return;

            var eventType=typeof(IEvent);

            var typesList = types.Where(t => !t.IsInterface &&
                                             !t.IsAbstract &&
                                             t.IsClass &&
                                             eventType.IsAssignableFrom(t));
            if (typesList.Any())
                this._store.AddRange(typesList);
        }

        public void Add(params Assembly[] assemblies)
        {
            if (assemblies != null && assemblies.Any())
                foreach (var assembly in assemblies)
                    this.Add(assembly.GetTypes());

        }

        public void Map(Type model, Type @event, HandlerEnum handle)
        {
            IList<Map> maps;
            if (!this._maps.TryGetValue(model, out maps))
            {
                maps = new List<Map>();
                this._maps[model] = maps;
            }

            if (!maps.Any(t => t.Type == @event))
            {
                maps.Add(new Map(@event, handle));
            }
            else
            {
                var map = maps.FirstOrDefault(t => t.Type == @event);
                map.Handle = handle;
            }
        }

        public void Build()
        {
            var modelType = typeof(ModelAttribute);

            foreach (var type in this._store.Where(t => t.IsDefined(modelType, false)))
            {
                var attributes = type.GetCustomAttributes<ModelAttribute>();
                foreach (var attribute in attributes)
                {
                    this.Map(attribute.Type, type, attribute.Handle);
                }
            }

            foreach (var mapList in this._maps)
            {
                foreach (var map in mapList.Value)
                {
                    var handle = map.GetHandle();
                    if (handle != null)
                    {
                        var handleType = handle.MakeGenericType(mapList.Key, map.Type);
                        this.MQ.SubscibeType(handleType);
                    }
                }
            }
        }

    }
}
