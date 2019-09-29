using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.ConfigServer.Repositorys
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : class
    {
        Task Add(TAggregateRoot aggregateRoot);
        Task Remove(TAggregateRoot aggregateRoot);
        Task Update(TAggregateRoot aggregateRoot);
        Task<TAggregateRoot> GetById(object id);
        Task<IEnumerable<TAggregateRoot>> GetALL();
    }
}
