using Microsoft.AspNetCore.Mvc;
using SAE.CommonComponent.UI.Models;
using SAE.CommonComponent.UI.Services;
using System.Linq;

namespace SAE.CommonComponent.UI.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly IComponentService _componentService;

        public LibraryController(ILibraryService libraryService,
                                 IComponentService componentService)
        {
            this._libraryService = libraryService;
            this._componentService = componentService;
        }
        

        public IActionResult Search()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Library library)
        {
            this._libraryService.Add(library);
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string name)
        {
            var lib = this._libraryService.Get(name);
            return View("add", lib);
        }

        [HttpPost]
        public IActionResult Edit(Library library)
        {
            this._libraryService.Update(library);
            return this.Ok();
        }

        public object ALL()
        {
            return this._libraryService.GetALL().Select(s => s.Name);
        }
        [HttpGet("~/api/[controller]")]
        public object Get()
        {
            return this._libraryService.GetALL();
        }

        [HttpGet("~/api/[controller]/{name}")]
        public object Get(string name)
        {
            return this._libraryService.Get(name);
        }
        [HttpGet("~/api/componentlibrary/{name}")]
        public object ComponentLibrary(string name)
        {
            var component = this._componentService.Get(name);
            return View();
        }
    }
}
