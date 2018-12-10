using System;
using System.Collections.Generic;
using System.Text;
using static SAE.CommonLibrary.EventStore.Queryable.Builder.RegistrationBuilder;

namespace SAE.CommonLibrary.EventStore.Queryable.Builder
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =false)]
    public sealed class ModelAttribute:Attribute
    {
        /// <summary>
        /// 处理的模型
        /// </summary>
        internal Type Type { get;}
        /// <summary>
        /// 处理方式
        /// </summary>
        public HandlerEnum Handle { get; set; }
        /// <summary>
        /// 标识一个使用默认处理事件对象
        /// </summary>
        /// <param name="typeName">类型名称，最好使用完全限定名</param>
        public ModelAttribute(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentNullException(nameof(typeName));

            var type= Type.GetType(typeName);
            
            this.Type = type ?? throw new ArgumentException($"{typeName}不存在");
        }

        public ModelAttribute(string typeName,HandlerEnum @enum):this(typeName)
        {
            this.Handle = @enum;
        }
    }
}
