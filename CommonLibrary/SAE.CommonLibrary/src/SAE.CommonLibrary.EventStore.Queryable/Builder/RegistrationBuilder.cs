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
    /// 注册构建者
    /// </summary>
    internal class RegistrationBuilder : IRegistrationBuilder
    {

        internal RegistrationBuilder(IMQ mq)
        {
            this.MQ = mq;
            this._store = new List<Type>();
            this._maps = new Dictionary<Type, IList<Type>>();
        }

        public RegistrationBuilder(IMQ mq, params Assembly[] assemblies) : this(mq)
        {
            this.Add(assemblies);
        }

        public readonly List<Type> _store;

        internal IMQ MQ { get; }

        private readonly IDictionary<Type, IList<Type>> _maps;

        public RegistrationBuilder(IMQ mq, params Type[] types) : this(mq)
        {
            this.Add(types);
        }

        public void Add(params Type[] types)
        {
            if (types == null || !types.Any()) return;

            var typesList = types.Where(t => !t.IsInterface &&
                                        !t.IsAbstract &&
                                        t.IsClass &&
                                        !t.IsValueType &&
                                        t.IsSubclassOf(typeof(IEvent)));
            if (typesList.Any())
                this._store.AddRange(typesList);
        }

        public void Add(params Assembly[] assemblies)
        {
            if (assemblies != null && assemblies.Any())
                foreach (var assembly in assemblies)
                    this.Add(assembly.GetTypes());

        }

        public void Map(Type model, Type @event)
        {
            IList<Type> list;
            if (!this._maps.TryGetValue(model, out list))
            {
                list = new List<Type>();
                this._maps[model] = list;
            }

            if (!list.Any(t => t == @event))
            {
                list.Add(@event);
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
                    IList<Type> list;

                    if (!this._maps.ContainsKey(attribute.Type))
                    {
                        list = new List<Type>();
                        this._maps[attribute.Type] = list;
                    }
                    else
                    {
                        list = this._maps[attribute.Type];
                    }

                    if (!list.Any(s => s == attribute.Type))
                    {
                        list.Add(attribute.Type);
                    }
                }
            }

            foreach (var map in this._maps)
            {
                foreach (var @event in map.Value)
                {
                    var handle = this.GetHandle(@event);
                    if (handle != null)
                    {
                        var handleType = handle.MakeGenericType(map.Key, @event);
                        this.MQ.SubscibeType(handleType);
                    }
                }
            }
        }

        public enum HandlerEnum : int
        {
            Add,
            Update,
            Remove
        }

        private Type GetHandle(Type type)
        {

            var @enum = this.GetEnum(type);
            Type handleType = null;
            switch (@enum)
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

        public HandlerEnum GetEnum(Type type)
        {
            if (type.Name.StartsWith(nameof(HandlerEnum.Add)))
            {
                return HandlerEnum.Add;
            }

            if (type.Name.StartsWith(nameof(HandlerEnum.Remove)))
            {
                return HandlerEnum.Remove;
            }

            if (type.Name.StartsWith(nameof(HandlerEnum.Update)))
            {
                return HandlerEnum.Update;
            }

            throw new Exception(
                $"Queryable.Handle，必须以{nameof(HandlerEnum.Add)},{nameof(HandlerEnum.Update)},{nameof(HandlerEnum.Remove)}结尾");
        }
    }
}
