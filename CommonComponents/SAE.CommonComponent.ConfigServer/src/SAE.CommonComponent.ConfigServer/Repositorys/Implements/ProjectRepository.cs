using Microsoft.Extensions.Configuration;
using SAE.CommonComponent.ConfigServer.Models;
using SAE.CommonComponent.ConfigServer.Repositorys.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.ConfigServer.Repositorys
{
    public class ProjectRepository : Repository<Project>
    {
        public ProjectRepository(IConfiguration configuration) : base(configuration)
        {
            this.InsertSql = "INSERT INTO project(id,name,solutionid,version,createtime) VALUES (@id,@name,@solutionid,@version,@createtime)";
            this.DeleteSql = "DELETE project WHERE id=@id";
            this.UpdateSql = "UPDATE project SET name=@name,solutionid=@solutionid,version=@version WHERE id=@id";
            this.GetByIdSql = "SELECT * FROM project WHERE id=@id";
            this.GetALLSql = "SELECT * FROM project";
        }
    }
}
