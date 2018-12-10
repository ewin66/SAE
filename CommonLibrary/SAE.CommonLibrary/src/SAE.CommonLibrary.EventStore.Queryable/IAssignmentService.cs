using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable
{
    /// <summary>
    /// replace service
    /// </summary>
    public interface IAssignmentService
    {
        /// <summary>
        /// 将<paramref name="source"/>对象的属性赋予到<paramref name="target"/>中
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        void Transfer(object source, object target);
    }
}
