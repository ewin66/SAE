using Microsoft.AspNetCore.Mvc;
using SAE.CommonComponent.UI.Models;
using SAE.CommonComponent.UI.Services;
using System.Linq;

namespace SAE.CommonComponent.UI.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            this._libraryService = libraryService;
        }
        public object ALL()
        {
            return this._libraryService.GetALL().Select(s => s.Name);
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
    }
}
