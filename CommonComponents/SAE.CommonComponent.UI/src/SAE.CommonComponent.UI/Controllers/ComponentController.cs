using Microsoft.AspNetCore.Mvc;
using SAE.CommonComponent.UI.Services;
using System.Linq;

namespace SAE.CommonComponent.UI.Controllers
{
    public class ComponentController : Controller
    {
        private readonly IComponentService _componentService;

        public ComponentController(IComponentService componentService)
        {
            this._componentService = componentService;
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
            this._componentService.Add(component);
            return View();
        }

        public object Types()
        {
            return this._componentService.Types();
        }

        public IActionResult Collection()
        {
            this.ViewData.Model = this._componentService.GetALL()
                                                        .Select(s => s.Name);

            return View();
        }
    }
}
