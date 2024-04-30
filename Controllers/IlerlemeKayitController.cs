using Deneme3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Deneme3.Controllers
{
    public class IlerlemeKayitController : Controller
    {
        public static int kullaniciID=DanisanController.kullaniciID;
        Context c = new Context();
        IlerlemeKayit ilerleme = new IlerlemeKayit();
        public IlerlemeKayitController()
        {

            var degerler = c.Danisans.FirstOrDefault(x => x.KisiId == kullaniciID);
            Console.WriteLine(degerler.GetType());
            
            ViewBag.KullaniciBilgileri = degerler;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IlerlemeKayitSayfa()
        {
            
            return View(ilerleme);
        }
        public IActionResult IlerlemeKayitlari()
        {
            var planlar = c.IlerlemeKayits.Where(x => x.DanisanID == kullaniciID).ToList(); ;
            return View(planlar);

        }

        [HttpPost]
        public IActionResult BtnIlerlemeEkle(IlerlemeKayit model)
        {
            ModelState.Remove("KayıtID");
            ModelState.Remove("DanisanID");
            ModelState.Remove("time");

            model.DanisanID = kullaniciID;
            

            c.IlerlemeKayits.Add(model);
            c.SaveChanges();

            return View("IlerlemeKayitSayfa", model);

        }

    }
}