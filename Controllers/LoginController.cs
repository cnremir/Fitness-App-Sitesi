using Deneme3.Models;
using Microsoft.AspNetCore.Mvc;
namespace Deneme3.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();


        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var admin = c.Admins.FirstOrDefault(x => x.KullaniciAdi == username && x.Sifre == password);
            if (admin != null)
            {
                
                return RedirectToAction("Index", "Admin");
            }
            var kisi = c.Kisis.FirstOrDefault(x => x.KullaniciAdi == username && x.Sifre == password);
            if (kisi != null)
            {

                var antrenor = c.Antrenors.FirstOrDefault(x => x.KisiId == kisi.KisiId);
                if (antrenor != null)
                {
                    AntrenorController.kullaniciID = kisi.KisiId;
                    return RedirectToAction("Index", "Antrenor");
                }
                var danisan = c.Danisans.FirstOrDefault(x => x.KisiId == kisi.KisiId);
                if (danisan != null)
                {
                    DanisanController.kullaniciID = kisi.KisiId;
                    return RedirectToAction("DanisanAnaSayfa", "Danisan");
                }
            }


            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(string username, string email, string password, string kullanicituru)
        {
            if (kullanicituru == "antrenor")
            {
                AntrenorController.suankiAntrenor.KullaniciAdi = username;
                AntrenorController.suankiAntrenor.Sifre = password;
                AntrenorController.suankiAntrenor.Email = email;
                return RedirectToAction("HesapOlustur", "Antrenor");
            }
            else
            {
                DanisanController.danisan.KullaniciAdi = username;
                DanisanController.danisan.Sifre = password;
                DanisanController.danisan.Email = email;
                return RedirectToAction("DanisanHesapOlustur", "Danisan");

            }

            return View();
        }
        public ActionResult sifreResetle()
        {

            return View();
        }
        [HttpPost]
        public IActionResult sifreResetle(string username, string email, string password, string kullanicituru)
        {

            var user = c.Kisis.FirstOrDefault(x => x.KullaniciAdi == username && x.Email == email);

            if (user != null)
            {

                user.Sifre = password;
                c.SaveChanges();

                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "KİŞİ BULUNAMADI");
                return RedirectToAction("sifreResetle");
            }
        }




    }
}
