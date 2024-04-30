using Deneme3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Deneme3.Controllers
{
    public class AdminController : Controller
    {
        Context db = new Context();
        public IActionResult Index()
        {
            var kisiler = db.Kisis.ToList();
            return View(kisiler);
        }
        public IActionResult Planlar()
        {
            var planlar = db.Plans.ToList();
            return View(planlar);
        }
        public IActionResult PlanDuzenle(int planID)
        {
            var plan = db.Plans.FirstOrDefault(x => x.planID == planID);
            return View("PlanDuzenle", plan);
        }
        [HttpPost]
        public ActionResult BtnPlanDuzenle(Plan model)
        {
            ModelState.Remove("olusturanKisiID");
            if (ModelState.IsValid)
            {
                // Veritabanından antrenörü alın
                Plan existingPlan = db.Plans.FirstOrDefault(x => x.planID == model.planID);

                if (existingPlan != null)
                {
                    // Değişiklikleri uygulayın
                    existingPlan.planAdi = model.planAdi;

                    existingPlan.planTuru = model.planTuru;
                    existingPlan.planHedefi = model.planHedefi;
                    existingPlan.baslangicTarihi = model.baslangicTarihi;
                    existingPlan.planDetay = model.planDetay;
                    existingPlan.sureHafta = model.sureHafta;

                    existingPlan.planDetay = model.planDetay;

                    db.Entry(existingPlan).State = EntityState.Modified;
                    db.SaveChanges();
                    // Veritabanını güncelleyin


                    // Başarılı bir şekilde güncelleme yapıldıktan sonra isteğin sonucunu döndürün
                    return RedirectToAction("Planlar", "Admin");
                }
                else
                {
                    // Antrenör bulunamadı hatası veya başka bir hata durumunda
                    ModelState.AddModelError("", "Antrenör bulunamadı veya bir hata oluştu.");
                }
            }

            // Model geçerli değilse, hataları göster
            return RedirectToAction("Planlar", "Admin");
        }
        public IActionResult KisiDuzenle(int kisiID)
        {
            var kisi = db.Kisis.FirstOrDefault(x => x.KisiId == kisiID);
            return View("KisiDuzenle", kisi);
        }
        [HttpPost]
        public ActionResult DeletePlan(int planId)
        {
            // Retrieve the plan based on the provided id
            var plan = db.Plans.FirstOrDefault(p => p.planID == planId);



            // Perform the deletion (in-memory, replace with your actual data source)
            db.Plans.Remove(plan);
            db.SaveChanges();

            // Return success status or any relevant information
            return RedirectToAction("Planlar", "Admin");
        }
        [HttpPost]
        public ActionResult BtnKisiDuzenle(Kisi model)
        {

            ModelState.Remove("ProfilFotografiYolu");
            if (ModelState.IsValid)
            {
                // Veritabanından antrenörü alın
                Kisi existingKisi = db.Kisis.Where(x => x.KisiId == model.KisiId).FirstOrDefault();

                if (existingKisi != null)
                {
                    // Değişiklikleri uygulayın
                    existingKisi.Adi = model.Adi;
                    existingKisi.Soyadi = model.Soyadi;
                    existingKisi.Yas = model.Yas;
                    existingKisi.KullaniciAdi = model.KullaniciAdi;
                    existingKisi.Email = model.Email;
                    existingKisi.Sifre = model.Sifre;



                    existingKisi.DogumTarihi = model.DogumTarihi;
                    existingKisi.TelNumarasi = model.TelNumarasi;
                    existingKisi.Cinsiyet = model.Cinsiyet;

                    // Veritabanını güncelleyin
                    db.Entry(existingKisi).State = EntityState.Modified;
                    db.SaveChanges();

                    // Başarılı bir şekilde güncelleme yapıldıktan sonra isteğin sonucunu döndürün
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    // Antrenör bulunamadı hatası veya başka bir hata durumunda
                    ModelState.AddModelError("", "Antrenör bulunamadı veya bir hata oluştu.");
                }
            }

            // Model geçerli değilse, hataları göster
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult DeleteKisi(int kisiID)
        {
            // Retrieve the plan based on the provided id
            var kisi = db.Kisis.FirstOrDefault(p => p.KisiId == kisiID);



            
            var antrenor = db.Antrenors.FirstOrDefault(p => p.KisiId == kisiID);
            if (antrenor != null)
            {
                db.Antrenors.Remove(antrenor);
                db.SaveChanges();
            }
            var danisan = db.Danisans.FirstOrDefault(p => p.KisiId == kisiID);
            
            if (danisan != null)
            {
                var kayit = db.IlerlemeKayits.Where(p => p.DanisanID == danisan.DanisanID).ToList();
                if (kayit != null)
                {
                    db.IlerlemeKayits.RemoveRange(kayit);
                    db.SaveChanges();
                }
                
            }

            if (antrenor != null)
            {
                var danisanantrenors = db.Danisan_Antrenors.FirstOrDefault(p => p.AntrenorID == antrenor.AntrenorID);
                if (danisanantrenors != null)
                    db.Danisan_Antrenors.Remove(danisanantrenors);
                db.SaveChanges();
            }
            else if (danisan != null)
            {

                var danisanantrenors = db.Danisan_Antrenors.FirstOrDefault(p => p.DanisanID == danisan.DanisanID);
                if (danisanantrenors != null)
                    db.Danisan_Antrenors.Remove(danisanantrenors);
                db.SaveChanges();
            }
            
            if (danisan != null)
            {
                db.Danisans.Remove(danisan);
                db.SaveChanges();
            }
                
            var planlar = db.Plans.Where(p => p.olusturanKisiID == kisiID).ToList();
            if (planlar != null)
            {
                db.Plans.RemoveRange(planlar);
                db.SaveChanges();
            }
               
            db.Kisis.Remove(kisi);
            db.SaveChanges();



            // Perform the deletion (in-memory, replace with your actual data source)


            // Return success status or any relevant information
            return RedirectToAction("Index", "Admin");
        }

    }
}


