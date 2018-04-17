using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore
{
    public interface IUnitOfWork:IDisposable
    {
        /// <summary>
        /// 回滚
        /// </summary>
        void RollBack();
    }
}
