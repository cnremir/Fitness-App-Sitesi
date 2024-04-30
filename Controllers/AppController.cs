using Deneme3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
namespace Deneme3.Controllers
{
    public class AppController : Controller
    {
        public static int kullaniciID;
        Context c=new Context();
        
        public IActionResult Index()
        {
            
            
            var degerler=c.Kisis.FirstOrDefault(x=> x.KisiId==kullaniciID);
            
            return View(degerler);
        }
    }
}
