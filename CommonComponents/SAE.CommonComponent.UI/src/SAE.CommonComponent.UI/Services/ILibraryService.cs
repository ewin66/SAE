using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SAE.CommonComponent.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAE.CommonComponent.UI.Services
{
    public interface ILibraryService
    {

        IEnumerable<Library> GetALL();
        Library Get(string name);
        void Add(Library library);
        void Update(Library library);
        void Remove(Library library);
    }

    public class LibraryService : ILibraryService
    {

        public readonly string _path;
        private readonly IJsonHelper _jsonHelper;

        public LibraryService(IHostingEnvironment hostingEnvironment, IJsonHelper jsonHelper)
        {
            _path = Path.Combine(hostingEnvironment.WebRootPath, "storage", "libs");
            this._jsonHelper = jsonHelper;
        }

        public void Add(Library library)
        {
            library.Unique(this.Get);
            this.Save(library);
        }

        public Library Get(string name)
        {
            return this.GetALL().FirstOrDefault(s => s.Name == name);
        }

        public IEnumerable<Library> GetALL()
        {
            var libs = Directory.GetFiles(_path, "*.json", SearchOption.AllDirectories)
                                .Select(s => JsonConvert.DeserializeObject<Library>(File.ReadAllText(s)))
                                .OrderBy(s => s.Name);
            return libs;
        }

        public void Remove(Library library)
        {
            var path = Path.Combine(_path, $"{library.Name}.json");
            File.Delete(path);
        }

        public void Update(Library library)
        {
            this.Save(library);
        }

        protected virtual void Save(Library library)
        {
            var path = Path.Combine(_path, $"{library.Name}.json");

            var json = this._jsonHelper.Serialize(library).ToString();

            var dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(path, json, Encoding.UTF8);
        }
    }
}
