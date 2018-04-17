using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace SAE.CommonLibrary.EventStore.Document.Dapper
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _dbConfiguration;
        public DbConnectionFactory(IConfiguration configuration)
        {
            _dbConfiguration = configuration.GetSection("DbConnection");
        }
        public IDbConnection Get(string name)
        {
            var connectionString = _dbConfiguration[name];
            return new MySqlConnection(connectionString);
        }
    }
}
