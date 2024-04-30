using Deneme3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Deneme3.Controllers
{
    public class AntrenorController : Controller
    {
        public static int kullaniciID;
        public static Antrenor suankiAntrenor=new Antrenor();
        public static Kisi mesajkisikullanici=new Kisi();
        public static Kisi mesajkisi2 = new Kisi();
        
        Context db = new Context();

        public IActionResult Index()
        {
            var suankiantrenor=db.Antrenors.FirstOrDefault(x=>x.KisiId==kullaniciID);
            suankiAntrenor.AntrenorID = suankiantrenor.AntrenorID;
            var degerler = db.Kisis.FirstOrDefault(x=>x.KisiId==kullaniciID);


            return View(degerler);
        }
        public IActionResult Mesaj(int KisiId)
        {
            
            var mesajkisikullanici1= db.Kisis.FirstOrDefault(x=>x.KisiId == kullaniciID);
            mesajkisikullanici = mesajkisikullanici1;
            var mesajkisi22= db.Kisis.FirstOrDefault(x=>x.KisiId == KisiId);
            mesajkisi2 = mesajkisi22;
            var mesajlar=db.Mesajlars.ToList();
            return View(mesajlar);
        }
            public IActionResult Profil()
        {
            var degerler = db.Kisis.FirstOrDefault(x => x.KisiId == kullaniciID);
            
            
            return View(degerler);
        }
        [HttpPost]
        public IActionResult MesajGonder(string Mesaj)
        {
            Mesajlar model=new Mesajlar();
            model.Tarih=DateTime.Now;
            model.AliciID = mesajkisi2.KisiId;
            model.GonderenID = mesajkisikullanici.KisiId;
            model.Mesaj = Mesaj;
            db.Mesajlars.Add(model);
            db.SaveChanges();
            return RedirectToAction("Danisanlar", "Antrenor");
        }
        [HttpPost]
        public ActionResult BtnProfilDuzenle(Antrenor model)
        {
            ModelState.Remove("Email");
            ModelState.Remove("Sifre");
            ModelState.Remove("Kisi");
            ModelState.Remove("KullaniciAdi");
            ModelState.Remove("ProfilFotografiYolu");
            if (ModelState.IsValid)
            {
                // Veritabanından antrenörü alın
                Antrenor existingAntrenor = GetAntrenorFromDatabase(model.AntrenorID);

                if (existingAntrenor != null)
                {
                    // Değişiklikleri uygulayın
                    existingAntrenor.Adi = model.Adi;
                    existingAntrenor.Soyadi = model.Soyadi;
                    existingAntrenor.Yas = model.Yas;
                    existingAntrenor.UzmanlikAlani = model.UzmanlikAlani;
                    existingAntrenor.Deneyimleri = model.Deneyimleri;
                    
                    existingAntrenor.DogumTarihi = model.DogumTarihi;
                    existingAntrenor.TelNumarasi = model.TelNumarasi;
                    existingAntrenor.Cinsiyet = model.Cinsiyet;

                    // Veritabanını güncelleyin
                    SaveAntrenorToDatabase(existingAntrenor);

                    // Başarılı bir şekilde güncelleme yapıldıktan sonra isteğin sonucunu döndürün
                    return RedirectToAction("Danisanlar");
                }
                else
                {
                    // Antrenör bulunamadı hatası veya başka bir hata durumunda
                    ModelState.AddModelError("", "Antrenör bulunamadı veya bir hata oluştu.");
                }
            }

            // Model geçerli değilse, hataları göster
            return View("Index", model);
        }
        [HttpPost]
        public ActionResult ProfilGoruntule(int KisiId)
        {
            // Retrieve the plan based on the provided id
            var danisan = db.Danisans.FirstOrDefault(p => p.KisiId == KisiId);

         
            return View(danisan);
        }
        [HttpPost]
        public ActionResult DanisanEkle(int KisiId)
        {
            // Retrieve the plan based on the provided id
            var danisan = db.Danisans.FirstOrDefault(p => p.KisiId == KisiId);
            Danisan_Antrenor iliski=new Danisan_Antrenor();
            iliski.DanisanID = danisan.DanisanID;
            iliski.AntrenorID = suankiAntrenor.AntrenorID;
            db.Danisan_Antrenors.Add(iliski);
            db.SaveChanges();

            return RedirectToAction("SeninDanisanlar","Antrenor");
        }

        private Antrenor GetAntrenorFromDatabase(int antrenorId)
        {
            // Veritabanından antrenörü ID ile alın
            return db.Antrenors.FirstOrDefault(x => x.KisiId == kullaniciID); ;
        }

        private void SaveAntrenorToDatabase(Antrenor antrenor)
        {
            // Veritabanına antrenörü kaydedin
            db.Entry(antrenor).State = EntityState.Modified;
            db.SaveChanges();
        }
        private void YeniAntrenorEkle(Antrenor antrenor)
        {
            // Veritabanına antrenörü kaydedin
            
            db.Entry(antrenor).State = EntityState.Added;
            db.SaveChanges();
        }


        
        public IActionResult Danisanlar()
        {
            var degerler = db.Danisans.ToList();
            
            return View(degerler);
        }
        public IActionResult SeninDanisanlar()
        {
            var degerler = db.Danisan_Antrenors
                                            .Where(da => da.AntrenorID == suankiAntrenor.AntrenorID)
                                            .Join(db.Danisans,
                                            da => da.DanisanID,
                                            d => d.DanisanID,
                                            (da, d) => d)
                                            .ToList();
            Console.WriteLine(degerler);
            return View(degerler);
        }

        public IActionResult Planlar()
        {
            var planlar=db.Plans.Where(x=>x.olusturanKisiID==kullaniciID).ToList();
            return View(planlar);
        }
        public IActionResult PlanOlustur()
        {
            var antrenorid = db.Antrenors.Where(x => x.KisiId == kullaniciID).FirstOrDefault();

            var danisanidler = db.Danisan_Antrenors
                        .Where(x => x.AntrenorID == antrenorid.AntrenorID)
                        .Select(x => x.DanisanID)
                        .ToList();
            
            
            
                List<Danisan> danisan = db.Danisans
                        .Where(x => danisanidler.Contains(x.DanisanID))
                        .ToList();

              
            

            return View(danisan);
        }
        public IActionResult ProfilDuzenle()
        {
            var degerler = db.Kisis.FirstOrDefault(x => x.KisiId == kullaniciID);
            return View(degerler);
        }
        public IActionResult HesapOlustur()
        {
            return View(suankiAntrenor);
        }
        public IActionResult BtnHesapOlustur(Antrenor model)
        {
        ModelState.Remove("Email");
        ModelState.Remove("Sifre");
        ModelState.Remove("Kisi");
        ModelState.Remove("KullaniciAdi");
        ModelState.Remove("ProfilFotografiYolu");
        ModelState.Remove("KisiID");
        if (ModelState.IsValid)
        {
                // Veritabanından antrenörü alın
                Antrenor existingAntrenor = new Antrenor();
            

            if (existingAntrenor != null)
            {
                    // Değişiklikleri uygulayın
                    existingAntrenor.Adi = model.Adi;
                    existingAntrenor.Soyadi = model.Soyadi;
                    existingAntrenor.Yas = model.Yas;
                    existingAntrenor.UzmanlikAlani = model.UzmanlikAlani;
                    existingAntrenor.Deneyimleri = model.Deneyimleri;
                    if (model.ProfilFotografiYolu == String.Empty || model.ProfilFotografiYolu==null)
                    {
                        existingAntrenor.ProfilFotografiYolu = 
                    "https://cdn-icons-png.flaticon.com/512/6522/6522516.png";
                    }
                    else
                    {
                        existingAntrenor.ProfilFotografiYolu = model.ProfilFotografiYolu;
                    }
                    
                    existingAntrenor.DogumTarihi = model.DogumTarihi;
                    existingAntrenor.TelNumarasi = model.TelNumarasi;
                    existingAntrenor.Cinsiyet = model.Cinsiyet;
                    existingAntrenor.KullaniciAdi = suankiAntrenor.KullaniciAdi;
                    existingAntrenor.Sifre = suankiAntrenor.Sifre;
                    existingAntrenor.Email = suankiAntrenor.Email;

                    // Veritabanını güncelleyin
                    
                    db.Antrenors.Add(existingAntrenor);
                    db.SaveChanges();
                    kullaniciID = db.Kisis.FirstOrDefault(x => x.KullaniciAdi == existingAntrenor.KullaniciAdi).KisiId;

                    // Başarılı bir şekilde güncelleme yapıldıktan sonra isteğin sonucunu döndürün
                    return RedirectToAction("Index");
            }
            else
            {
                // Antrenör bulunamadı hatası veya başka bir hata durumunda
                ModelState.AddModelError("", "Antrenör bulunamadı veya bir hata oluştu.");
            }
        }

        // Model geçerli değilse, hataları göster
        return View();
        }
        [HttpPost]
        public IActionResult PlanOlustur(string planname, string plantype,string plangoal,DateTime planstart,int planduration,string plandetails,int danisan)
        {
            Plan yeniplan = new Plan();
            yeniplan.planAdi = planname;
            yeniplan.olusturanKisiID = kullaniciID;
            yeniplan.planTuru = plantype;
            yeniplan.planHedefi = plangoal;
            yeniplan.baslangicTarihi = planstart;
            yeniplan.sureHafta = planduration;
            yeniplan.planDetay = plandetails;
            yeniplan.atananDanisanID = danisan;
            var vardanisan = db.Danisans.Where(a => a.DanisanID == danisan).FirstOrDefault();
            yeniplan.atananDanisanAdSoyadi = vardanisan.Adi + " " + vardanisan.Soyadi;
            db.Plans.Add(yeniplan);
            db.SaveChanges();

            
            return RedirectToAction("Planlar","Antrenor");
        }
        

        // POST: Antrenor/DeletePlan/1
        [HttpPost]
        public ActionResult DeletePlan(int planId)
        {
            // Retrieve the plan based on the provided id
            var plan = db.Plans.FirstOrDefault(p => p.planID == planId);

            

            // Perform the deletion (in-memory, replace with your actual data source)
            db.Plans.Remove(plan);
            db.SaveChanges();

            // Return success status or any relevant information
            return RedirectToAction("Planlar", "Antrenor");
        }
        [HttpPost]
        public ActionResult DanisanSil(int KisiId)
        {
            // Retrieve the plan based on the provided id
            var iliski=db.Danisan_Antrenors.FirstOrDefault(x=>x.DanisanID==KisiId);



            // Perform the deletion (in-memory, replace with your actual data source)
            db.Danisan_Antrenors.Remove(iliski);
            db.SaveChanges();

            // Return success status or any relevant information
            return RedirectToAction("Danisanlar", "Antrenor");
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
            ModelState.Remove("atananDanisanID");
            ModelState.Remove("atananDanisanAdSoyadi");
            if (ModelState.IsValid)
            {
                // Veritabanından antrenörü alın
                Plan existingPlan = db.Plans.FirstOrDefault(x=>x.planID==model.planID);

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
                    return RedirectToAction("Planlar","Antrenor");
                }
                else
                {
                    // Antrenör bulunamadı hatası veya başka bir hata durumunda
                    ModelState.AddModelError("", "Antrenör bulunamadı veya bir hata oluştu.");
                }
            }

            // Model geçerli değilse, hataları göster
            return View("Index","Antrenor");
        }
    }

    
}
