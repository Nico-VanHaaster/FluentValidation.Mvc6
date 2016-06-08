using Microsoft.AspNetCore.Mvc;

namespace FluentValidation.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Models.BasicValidationModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Index(Models.BasicValidationModel model)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("", "There was an error validating the model state.");

            return View(model);
        }
    }
}
