using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.UI.Controllers
{
    public class ComponentController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ComponentController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Models.Component component)
        {
            return View();
        }

        public object Types()
        {
            var path = Path.Combine(this._hostingEnvironment.WebRootPath, "storage", "component");
            var dir = Directory.GetDirectories(path, string.Empty, SearchOption.AllDirectories)
                               .Select(s => s.Substring(path.Length).Replace("\\", "/").Trim('/'))
                               .OrderBy(s => s);
            return dir;
        }

        public IActionResult Collection()
        {
            var path = Path.Combine(this._hostingEnvironment.WebRootPath, "storage", "component");
            this.ViewData.Model = Directory.GetFiles(path, "*.js", SearchOption.AllDirectories)
                                          .Select(s => s.Substring(path.Length - "template".Length - 1).Replace("\\", "/").Trim('/').Replace(".js", string.Empty))
                                          .OrderBy(s => s);
            return View();
        }

        public IActionResult Libs()
        {
            var path = Path.Combine(this._hostingEnvironment.WebRootPath, "storage", "lib");
            var libs = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories)
                               .Select(s=>Path.GetFileNameWithoutExtension(s))
                               .OrderBy(s => s);
            return this.Json(libs);
        }

        public IActionResult Preview()
        {
            return View();
        }
        
    }
}
