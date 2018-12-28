using SAE.CommonLibrary.ObjectMapper;
namespace SAE.CommonLibrary.EventStore.Queryable.Default
{
    public class TransferService : IAssignmentService
    {
        public void Transfer(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            if (!TinyMapper.BindingExists(sourceType, targetType))
            {
                TinyMapper.Bind(sourceType, targetType);
            }
            TinyMapper.Map(source.GetType(), target.GetType(), source, target);
        }
    }
}
