using Deneme3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Deneme3.Controllers
{
    public class DanisanController : Controller
    {
        public static int kullaniciID;
        Context c = new Context();
        public static Danisan danisan = new Danisan();
        public static Kisi mesajkisikullanici = new Kisi();
        public static Kisi mesajkisi2 = new Kisi();
        public IActionResult DanisanPlanlar()
        {


            var degerler = c.Kisis.FirstOrDefault(x => x.KisiId == kullaniciID);

            return View(degerler);
        }
        public ActionResult ProfilGoruntule(int KisiId)
        {
            // Retrieve the plan based on the provided id
            var antrenor = c.Antrenors.FirstOrDefault(p => p.KisiId == KisiId);


            return View(antrenor);
        }
        public IActionResult Mesajlar(int KisiId)
        {
            var mesajkisikullanici1 = c.Kisis.FirstOrDefault(x => x.KisiId == kullaniciID);
            mesajkisikullanici = mesajkisikullanici1;
            var mesajkisi22 = c.Kisis.FirstOrDefault(x => x.KisiId == KisiId);
            mesajkisi2 = mesajkisi22;
            var mesajlar = c.Mesajlars.ToList();
            return View(mesajlar);
        }
        public IActionResult Antrenorler()
        {
            var degerler = c.Antrenors.ToList();

            return View(degerler);
        }
        
        
        [HttpPost]
        public IActionResult MesajGonder(string Mesaj)
        {
            Mesajlar model = new Mesajlar();
            model.Tarih = DateTime.Now;
            model.AliciID = mesajkisi2.KisiId;
            model.GonderenID = mesajkisikullanici.KisiId;
            model.Mesaj = Mesaj;
            c.Mesajlars.Add(model);
            c.SaveChanges();
            return RedirectToAction("DanisanAnaSayfa", "Danisan");
        }


        public IActionResult DanisanAnaSayfa()
        {


            var degerler = c.Kisis.FirstOrDefault(x => x.KisiId == kullaniciID);

            return View(degerler);
        }
        public IActionResult DanisanProfil()
        {


            var degerler = c.Kisis.FirstOrDefault(x => x.KisiId == kullaniciID);

            return View(degerler);
        }
        public ActionResult BtnProfilDuzenle(Danisan model)
        {
            ModelState.Remove("Email");
            ModelState.Remove("Sifre");
            ModelState.Remove("Kisi");
            ModelState.Remove("KullaniciAdi");
            ModelState.Remove("ProfilFotografiYolu");
            
                // Veritabanından antrenörü alın
                Danisan existingDanisan = GetDanisanFromDatabase(model.DanisanID);

                if (existingDanisan != null)
                {
                    // Değişiklikleri uygulayın
                    existingDanisan.Adi = model.Adi;
                    existingDanisan.Soyadi = model.Soyadi;
                    existingDanisan.Yas = model.Yas;

                    existingDanisan.DogumTarihi = model.DogumTarihi;
                    existingDanisan.TelNumarasi = model.TelNumarasi;
                    existingDanisan.Cinsiyet = model.Cinsiyet;
                existingDanisan.Boy = model.Boy;
                existingDanisan.Kg = model.Kg;
                existingDanisan.Hedef = model.Hedef;
                // Veritabanını güncelleyin
                SaveDanisanToDatabase(existingDanisan);

                    // Başarılı bir şekilde güncelleme yapıldıktan sonra isteğin sonucunu döndürün
                    return RedirectToAction("DanisanProfil");
                }
                else
                {
                    // Antrenör bulunamadı hatası veya başka bir hata durumunda
                    ModelState.AddModelError("", "Danışan bilgisi bulunamadı veya bir hata oluştu.");
                return View("DanisanProfil", model);
                }
       
            
        }

        private Danisan GetDanisanFromDatabase(int danisanID)
        {
            // Veritabanından ID ile alın
            return c.Danisans.FirstOrDefault(x => x.KisiId == kullaniciID); ;
        }

        private void SaveDanisanToDatabase(Danisan danisan)
        {
            // Veritabanına antrenörü kaydedin
            c.Entry(danisan).State = EntityState.Modified;
            c.SaveChanges();
        }

        public IActionResult PlanGetir()
        {
            var danisan= c.Danisans.FirstOrDefault(x=>x.KisiId==kullaniciID);
            var planlar = c.Plans.Where(x => x.atananDanisanID == danisan.DanisanID).ToList(); ;
            
            return View(planlar);

        }
        public IActionResult DanisanHesapOlustur()
        {
            
            return View(danisan);
        }
        public IActionResult BtnHesapOlustur(Danisan model)
        {
            ModelState.Remove("Email");
            ModelState.Remove("Sifre");
          ModelState.Remove("Kisi");
           ModelState.Remove("KullaniciAdi");
            ModelState.Remove("ProfilFotografiYolu");
            ModelState.Remove("KisiID");
            
                // Veritabanından antrenörü alın
                Danisan existingDanisan = new Danisan();
           
                if (existingDanisan != null)
                {
                    // Değişiklikleri uygulayın
                    existingDanisan.Adi = model.Adi;
                    existingDanisan.Soyadi = model.Soyadi;
                    existingDanisan.Yas = model.Yas;
                if (model.ProfilFotografiYolu == String.Empty || model.ProfilFotografiYolu == null)
                {
                    existingDanisan.ProfilFotografiYolu =
                "https://cdn-icons-png.flaticon.com/512/6522/6522516.png";
                }
                else
                {
                    existingDanisan.ProfilFotografiYolu = model.ProfilFotografiYolu;
                }
                    existingDanisan.DogumTarihi = model.DogumTarihi;
                    existingDanisan.TelNumarasi = model.TelNumarasi;
                    existingDanisan.Cinsiyet = model.Cinsiyet;
                    existingDanisan.KullaniciAdi  = DanisanController.danisan.KullaniciAdi;
                    existingDanisan.Sifre = DanisanController.danisan.Sifre;
                    existingDanisan.Email = DanisanController.danisan.Email;
                existingDanisan.Boy = model.Boy;
                existingDanisan.Kg = model.Kg;
                existingDanisan.Hedef = model.Hedef;

                // Veritabanını güncelleyin

                c.Danisans.Add(existingDanisan);
                    c.SaveChanges();
                    kullaniciID = c.Kisis.FirstOrDefault(x => x.KullaniciAdi == existingDanisan.KullaniciAdi).KisiId;

                    // Başarılı bir şekilde güncelleme yapıldıktan sonra isteğin sonucunu döndürün
                    return RedirectToAction("Index","Login");
                }
                else
                {
                    // Antrenör bulunamadı hatası veya başka bir hata durumunda
                    ModelState.AddModelError("", "Antrenör bulunamadı veya bir hata oluştu.");
                }
            

            // Model geçerli değilse, hataları göster
            return View();
        }

       





    }
}

