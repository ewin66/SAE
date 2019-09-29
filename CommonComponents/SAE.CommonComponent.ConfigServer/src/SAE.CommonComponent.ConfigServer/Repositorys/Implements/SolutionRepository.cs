using Microsoft.Extensions.Configuration;
using SAE.CommonComponent.ConfigServer.Models;
using SAE.CommonComponent.ConfigServer.Repositorys.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.ConfigServer.Repositorys
{
    public class SolutionRepository : Repository<Solution>
    {
        public SolutionRepository(IConfiguration configuration) : base(configuration)
        {
            this.InsertSql = "INSERT INTO solution(name,createtime) VALUES (@name,@createtime)";
            this.DeleteSql = "DELETE solution WHERE id=@id";
            this.UpdateSql = "UPDATE solution SET name=@name,createtime=@createtime WHERE id=@id";
            this.GetByIdSql = "SELECT * FROM solution WHERE id=@id";
            this.GetALLSql = "SELECT * FROM solution";
        }
    }
}
