using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAE.CommonComponent.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAE.CommonComponent.UI.Services
{
    public interface IComponentService
    {
        IEnumerable<string> Types();
        IEnumerable<Component> GetALL();
        Component Get(string name);
        void Add(Component component);
        void Update(Component component);
        void Remove(Component component);
    }

    public class ComponentService : IComponentService
    {
        private readonly string _path;
        private readonly IJsonHelper _jsonHelper;

        public ComponentService(IHostingEnvironment hostingEnvironment, IJsonHelper jsonHelper)
        {
            _path = Path.Combine(hostingEnvironment.WebRootPath, "storage", "components");
            this._jsonHelper = jsonHelper;
        }


        public void Add(Component component)
        {
            this.Save(component);
        }

        public Component Get(string name)
        {
            return this.GetALL().FirstOrDefault(s => s.Name == name);
        }

        public IEnumerable<Component> GetALL()
        {

            var components = Directory.GetFiles(this._path, "*.js", SearchOption.AllDirectories)
                                      .Select(s => s.Substring(this._path.Length - "component".Length - 1).Replace("\\", "/").Trim('/').Replace(".js", string.Empty))
                                      .OrderBy(s => s)
                                      .Select(s => new Component
                                      {
                                          Name = s
                                      });
            return components;
        }

        public void Remove(Component component)
        {
            var path = Path.Combine(_path, $"{component.Name}.json");
            File.Delete(path);
        }

        public IEnumerable<string> Types()
        {
            var types = Directory.GetDirectories(this._path, string.Empty, SearchOption.AllDirectories)
                                 .Select(s => s.Substring(this._path.Length).Replace("\\", "/").Trim('/'))
                                 .OrderBy(s => s);
            return types;
        }

        public void Update(Component component)
        {
            this.Save(component);
        }

        protected virtual void Save(Component component)
        {

            var path = Path.Combine(_path, $"{component.Name}.json");

            var json = this._jsonHelper.Serialize(component).ToString();

            var dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(path, json, Encoding.UTF8);
        }
    }
}
