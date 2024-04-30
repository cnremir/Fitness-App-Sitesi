using Microsoft.AspNetCore.Mvc;

namespace Deneme3.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
