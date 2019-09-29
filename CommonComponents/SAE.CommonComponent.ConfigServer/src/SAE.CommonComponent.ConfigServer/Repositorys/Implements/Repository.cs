using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAE.CommonComponent.ConfigServer.Repositorys.Implements
{
    public class Repository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : class
    {
        protected string InsertSql;
        protected string DeleteSql;
        protected string UpdateSql;
        protected string GetByIdSql;
        protected string GetALLSql;
        private string _connectionString;
        public Repository(IConfiguration configuration)
        {
            this._connectionString = configuration.GetSection("ConnectionString").Get<string>();
        }

        public Task Add(TAggregateRoot aggregateRoot)
        {
            using (var db = new MySqlConnection(this._connectionString))
            {
                return db.ExecuteAsync(this.InsertSql, aggregateRoot);
            }
        }

        public Task<IEnumerable<TAggregateRoot>> GetALL()
        {
            using (var db = new MySqlConnection(this._connectionString))
            {
                return db.QueryAsync<TAggregateRoot>(this.GetALLSql);
            }
        }

        public Task<TAggregateRoot> GetById(object id)
        {
            using (var db = new MySqlConnection(this._connectionString))
            {
                return db.QueryFirstAsync<TAggregateRoot>(this.GetByIdSql, new { id });
            }
        }

        public Task Remove(TAggregateRoot aggregateRoot)
        {
            using (var db = new MySqlConnection(this._connectionString))
            {
                return db.ExecuteAsync(this.DeleteSql, aggregateRoot);
            }
        }

        public Task Update(TAggregateRoot aggregateRoot)
        {
            using (var db = new MySqlConnection(this._connectionString))
            {
                return db.ExecuteAsync(this.UpdateSql, aggregateRoot);
            }
        }
    }
}
