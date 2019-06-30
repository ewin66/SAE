using Microsoft.AspNetCore.Hosting;
using SAE.CommonComponent.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        private readonly IHostingEnvironment _hostingEnvironment;

        public ComponentService(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }


        public void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public Component Get(string name)
        {
            return this.GetALL().FirstOrDefault(s => s.Name == name);
        }

        public IEnumerable<Component> GetALL()
        {
            var path = Path.Combine(this._hostingEnvironment.WebRootPath, "storage", "component");
            var components = Directory.GetFiles(path, "*.js", SearchOption.AllDirectories)
                                      .Select(s => s.Substring(path.Length - "component".Length - 1).Replace("\\", "/").Trim('/').Replace(".js", string.Empty))
                                      .OrderBy(s => s)
                                      .Select(s => new Component
                                      {
                                          Name = s
                                      });
            return components;
        }

        public void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Types()
        {
            var path = Path.Combine(this._hostingEnvironment.WebRootPath, "storage", "component");
            var types = Directory.GetDirectories(path, string.Empty, SearchOption.AllDirectories)
                                 .Select(s => s.Substring(path.Length).Replace("\\", "/").Trim('/'))
                                 .OrderBy(s => s);
            return types;
        }

        public void Update(Component component)
        {
            throw new NotImplementedException();
        }
    }
}
