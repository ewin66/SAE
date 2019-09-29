using Microsoft.Extensions.Configuration;
using SAE.CommonComponent.ConfigServer.Models;
using SAE.CommonComponent.ConfigServer.Repositorys.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.ConfigServer.Repositorys
{
    public class ConfigRepository : Repository<Config>
    {
        public ConfigRepository(IConfiguration configuration) : base(configuration)
        {
            this.InsertSql = "INSERT INTO config(id,name,solutionid,templateid,content,version,createtime) VALUES (@id,@name,@solutionid,@templateid,@content,@version,@createtime)";
            this.DeleteSql = "DELETE config WHERE id=@id";
            this.UpdateSql = "UPDATE config SET name=@name,solutionid=@solutionid,templateid=@templateid,content=@content,version=@version WHERE id=@id";
            this.GetByIdSql = "SELECT * FROM config WHERE id=@id";
            this.GetALLSql = "SELECT * FROM config";
        }
    }
}
