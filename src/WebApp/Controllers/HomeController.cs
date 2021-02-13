using Microsoft.AspNetCore.Mvc;

namespace RESTAPI.Controllers{
    public class HomeController : Controller {
        [HttpGet]
        public IActionResult Index() {
            return View();
        }
    }
}