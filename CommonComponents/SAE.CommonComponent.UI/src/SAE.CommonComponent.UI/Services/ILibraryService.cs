using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SAE.CommonComponent.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public LibraryService(IHostingEnvironment hostingEnvironment,IJsonHelper jsonHelper)
        {
            _path = Path.Combine(hostingEnvironment.WebRootPath, "storage", "lib");
            this._jsonHelper = jsonHelper;
        }

        public void Add(Library library)
        {
            library.Unique(this.Get);
            var json = _jsonHelper.Serialize(library).ToString();
            File.WriteAllText(Path.Combine(this._path, $"{library.Name}.json"), json, System.Text.Encoding.UTF8);
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
            throw new NotImplementedException();
        }

        public void Update(Library library)
        {
            var json = _jsonHelper.Serialize(library).ToString();
            File.WriteAllText(Path.Combine(this._path, $"{library.Name}.json"), json, System.Text.Encoding.UTF8);
        }
    }
}
