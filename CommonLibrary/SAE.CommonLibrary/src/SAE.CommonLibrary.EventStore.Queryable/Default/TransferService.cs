using System;
using System.Collections.Generic;
using System.Text;
using Nelibur.ObjectMapper;

namespace SAE.CommonLibrary.EventStore.Queryable.Default
{
    public class TransferService : IAssignmentService
    {
        public void Transfer(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            TinyMapper.Bind(sourceType, targetType);
            TinyMapper.Map(source.GetType(), target.GetType(), source, target);
        }
    }
}
