using Microsoft.Extensions.Configuration;
using SAE.CommonComponent.ConfigServer.Models;
using SAE.CommonComponent.ConfigServer.Repositorys.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.ConfigServer.Repositorys
{
    public class TemplateRepository : Repository<Template>
    {
        public TemplateRepository(IConfiguration configuration) : base(configuration)
        {
            this.InsertSql = "INSERT INTO template(name,format,createtime) VALUES (@name,@format,@createtime)";
            this.DeleteSql = "DELETE template WHERE id=@id";
            this.UpdateSql = "UPDATE template SET name=@name,format=@format,createtime=@createtime WHERE id=@id";
            this.GetByIdSql = "SELECT * FROM template WHERE id=@id";
            this.GetALLSql = "SELECT * FROM template";
        }
    }
}
