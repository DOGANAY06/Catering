using Catering.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Catering.Controllers
{
    [Authorize(Roles ="Yonetici")]
    public class YonetimController : Controller
    {
        cateringserviceDBContext db = new cateringserviceDBContext();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Sayfalar()
        {//istediğimiz tabloyu sayfalarda çekicez 
            var sayfalar = db.Sayfalar.Where(s => s.Silindi == false).OrderBy(s => s.Baslik).ToList();
            //silinmeyen sayfaların başlığına göre sıralanarak gelsin
            return View(sayfalar);

        }

        public IActionResult SayfaEkle()
        {

            return View();
        }
        [HttpPost]
        public IActionResult SayfaEkle(Sayfalar s)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            s.Silindi = false; //gelen modelin silindisi false olsun
            db.Sayfalar.Add(s);
            db.SaveChanges();
            return RedirectToAction("Sayfalar");
        }
        public IActionResult SayfaGetir(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var sayfa = db.Sayfalar.Where(s => s.Silindi == false && s.SayfaId == id).FirstOrDefault();
            return View("SayfaGuncelle", sayfa);
        }
        public IActionResult SayfaGuncelle(Sayfalar syf)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var sayfa = db.Sayfalar.Where(s => s.Silindi == false && s.SayfaId == syf.SayfaId).FirstOrDefault();
            sayfa.Baslik = syf.Baslik;
            sayfa.Icerik = syf.Icerik;
            sayfa.Aktif = syf.Aktif;
            db.Sayfalar.Update(sayfa);
            db.SaveChanges();
            return RedirectToAction("Sayfalar");
        }

        public IActionResult SayfaSil(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var sayfa = db.Sayfalar.Where(s => s.Silindi == false && s.SayfaId == id).FirstOrDefault();
            sayfa.Silindi = true;

            db.Sayfalar.Update(sayfa);
            db.SaveChanges();
            return RedirectToAction("Sayfalar");
        }

        [HttpGet]
        public IActionResult Yorumlar()
        {
            var yorumlar = db.Yorum.Include(t => t.Tur).Include(u => u.Uye).Where(y => y.Silindi == false).OrderByDescending(y => y.Eklemetarihi).ToList();

            return View(yorumlar);
        }
        [HttpPost] //yorumları listelemek için
        public IActionResult Yorumlar(string listelemeturu)
        {
            var yorumlar = db.Yorum.Include(t => t.Tur).Include(u => u.Uye).Where(y => y.Silindi == false).OrderByDescending(y => y.Eklemetarihi).ToList();
            switch (listelemeturu)
            {
                case "Onayli":
                    yorumlar = db.Yorum.Where(y => y.Silindi == false && y.Aktif == true).OrderByDescending(y => y.Eklemetarihi).ToList();
                    //onayli olanlar için
                    break;
                case "Onaysiz":
                    yorumlar = db.Yorum.Where(y => y.Silindi == false && y.Aktif == false).OrderByDescending(y => y.Eklemetarihi).ToList();
                    break;


            }
            return View(yorumlar);
        }
        public IActionResult Onayla(int id)
        {
            var yorum = db.Yorum.Where(d => d.Silindi == false && d.YorumId == id).FirstOrDefault();
            yorum.Aktif = Convert.ToBoolean((-1 * Convert.ToInt32(yorum.Aktif)) + 1);
            //yorumumuz aktifte ise pasif pasifte ise aktif yapıcak

            db.Yorum.Update(yorum);
            db.SaveChanges();
            return RedirectToAction("Yorumlar");
        }
        public IActionResult YorumSil(int id)
        {
            var yorum = db.Yorum.Where(d => d.Silindi == false && d.YorumId == id).FirstOrDefault();
            yorum.Silindi = true;
            //yorumumuz aktifte ise pasif pasifte ise aktif yapıcak

            db.Yorum.Update(yorum);
            db.SaveChanges();
            return RedirectToAction("Yorumlar");
        }

        public IActionResult Tarifler()
        {//istediğimiz tabloyu sayfalarda çekicez 
            var tarifler = db.CateringDetail.Include(k => k.Catering
            ).Where(cd => cd.Silindi == false).OrderBy(cd => cd.Cateringfirmabilgi).ToList();
            //silinmeyen sayfaların başlığına göre sıralanarak gelsin
            //burada cateringid ekliyoruz include methodu ile
            return View(tarifler);

        }

        public IActionResult TarifEkle()
        {
            //cateringId leri için select list getircez 
            var caterings = (from k in db.Cateringler.Where(k => k.Silindi == false && k.Aktif == true).ToList()
                             select new SelectListItem
                             {
                                 Text = k.Cateringadi,
                                 Value = k.CateringId.ToString()
                             }
            ); //BURADA query kullandık catering adi ve ıd ye göre seçip ekleme yapmak için 
            ViewBag.CateringId = caterings;
            return View();
        }
        [HttpPost]
        public IActionResult TarifEkle(CateringDetail cd)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            cd.Silindi = false; //gelen modelin silindisi false olsun
            db.CateringDetail.Add(cd);
            db.SaveChanges();
            return RedirectToAction("Tarifler");
        }
        public IActionResult TarifGetir(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var tarif = db.CateringDetail.Include(k => k.Catering
            ).Where(cd => cd.Silindi == false && cd.TurId == id).FirstOrDefault();
            var caterings = (from k in db.Cateringler.Where(k => k.Silindi == false && k.Aktif == true).ToList()
                             select new SelectListItem
                             {
                                 Text = k.Cateringadi,
                                 Value = k.CateringId.ToString()
                             }
            ); //BURADA query kullandık catering adi ve ıd ye göre seçip ekleme yapmak için 
            return View("TarifGuncelle", tarif);
        }


        public IActionResult TarifYorumlari(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var yorumlar = db.Yorum.Where(cd => cd.Silindi == false && cd.TurId == id).ToList();
            //bütün kayıtları getiriyoruz
            return View("Yorumlar", yorumlar);
        }

        public IActionResult TarifGuncelle(CateringDetail cde)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var tarif = db.CateringDetail.Where(cd => cd.Silindi == false && cd.TurId == cde.TurId).FirstOrDefault();
            tarif.Cateringfirmabilgi = cde.Cateringfirmabilgi;
            tarif.Icerik = cde.Icerik;
            tarif.Sira = cde.Sira;
            tarif.CateringId = cde.CateringId;
            tarif.Aktif = cde.Aktif;
            db.CateringDetail.Update(tarif);
            db.SaveChanges();
            return RedirectToAction("Tarifler");
        }

        public IActionResult TarifSil(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var tarif = db.CateringDetail.Where(cd => cd.Silindi == false && cd.TurId == id).FirstOrDefault();
            tarif.Silindi = true;


            db.CateringDetail.Update(tarif);
            db.SaveChanges();
            return RedirectToAction("Tarifler");
        }


        public IActionResult Cateringler()
        {//istediğimiz tabloyu sayfalarda çekicez 
            var cateringler = db.Cateringler.Where(c => c.Silindi == false).OrderBy(c => c.Cateringadi).ToList();
            //silinmeyen sayfaların başlığına göre sıralanarak gelsin
            return View(cateringler);

        }

        public IActionResult CateringEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CateringEkle(Cateringler c)
        {//catering veritabanından geldiği için direk gönderebiliriizz
            c.Silindi = false; //gelen modelin silindisi false olsun
            db.Cateringler.Add(c);
            db.SaveChanges();
            return RedirectToAction("Cateringler");
        }
        public IActionResult CateringGetir(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var catering = db.Cateringler.Where(c => c.Silindi == false && c.CateringId == id).FirstOrDefault();
            return View("CateringGuncelle", catering);
        }
        public IActionResult CateringGuncelle(Cateringler ct)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var catering = db.Cateringler.Where(c => c.Silindi == false && c.CateringId == ct.CateringId).FirstOrDefault();
            catering.Cateringadi = ct.Cateringadi;

            catering.Aktif = ct.Aktif;
            db.Cateringler.Update(catering);
            db.SaveChanges();
            return RedirectToAction("Cateringler");
        }
        public IActionResult CateringSil(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var catering = db.Cateringler.Where(c => c.Silindi == false && c.CateringId == id).FirstOrDefault();
            catering.Silindi = true;


            db.Cateringler.Update(catering);
            db.SaveChanges();
            return RedirectToAction("Cateringler");
        }

        public IActionResult CateringDetailim(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var details = db.CateringDetail.Include(k => k.Catering
            ).Where(c => c.Silindi == false && c.CateringId == id).ToList();
            //bütün kayıtları getiriyoruz
            return View("Tarifler", details);
        }

        public IActionResult Bilgilerim()
        {
            int kulid = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var kullanici = db.Kullanicilar.Find(kulid);
            kullanici.Parola = "";
            return View(kullanici);
        }

        public IActionResult BilgilerimiGuncelle(Kullanicilar kul)
        {
            var kullanici = db.Kullanicilar.Where(cd => cd.Silindi == false && cd.KullaniciId == kul.KullaniciId).FirstOrDefault();
            kullanici.Adi = kul.Adi;
            kullanici.Soyadi = kul.Soyadi;
            kullanici.Eposta = kul.Eposta;
            kullanici.Telefon = kul.Telefon;
            //parola nullsa değiştirmicez degilse değişstiriz

            try
            {
                if (kul.Parola.Trim().Length != 0)
                {//boş  değilse  trim boşlukları kaldırıyor
                    kullanici.Parola = kul.Parola.Trim();
                }
            }
            catch
            {
            }

           
            db.Kullanicilar.Update(kullanici);
            db.SaveChanges();
            return RedirectToAction("Bilgilerim");
        }

        public IActionResult Kullanicilar()
        {//istediğimiz tabloyu sayfalarda çekicez 
            var kullanici = db.Kullanicilar.Where(cd => cd.Silindi == false).OrderBy(cd => cd.Yetki).OrderBy(cd => cd.Adi).OrderBy(cd => cd.Soyadi).ToList();
            //silinmeyen sayfaların başlığına göre sıralanarak gelsin
            //burada cateringid ekliyoruz include methodu ile
            return View(kullanici);

        }

        public IActionResult KullaniciEkle()
        {

            return View();
        }
        [HttpPost]
        public IActionResult KullaniciEkle(Kullanicilar k)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            k.Silindi = false; //gelen modelin silindisi false olsun
            k.Parola = k.Parola.Trim();
            db.Kullanicilar.Add(k);
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }
        public IActionResult KullaniciGetir(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var kullanici = db.Kullanicilar.Where(cd => cd.Silindi == false && cd.KullaniciId == id).FirstOrDefault();
            //kullanici.Parola = ""; // boş gelsin gözükmesin
            return View("KullaniciGuncelle", kullanici);
        }
        
        public IActionResult KullaniciGuncelle(Kullanicilar kul)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var kullanici = db.Kullanicilar.Where(cd => cd.Silindi == false && cd.KullaniciId == kul.KullaniciId).FirstOrDefault();
            kullanici.Aktif = kul.Aktif;
            kullanici.Adi = kul.Adi;
            kullanici.Soyadi = kul.Soyadi;
            kullanici.Eposta = kul.Eposta;
            kullanici.Telefon = kul.Telefon;
            //parola nullsa değiştirmicez degilse değişstiriz
            
            try
            {
                if (kul.Parola.Trim().Length!=0)
                {//boş  değilse  trim boşlukları kaldırıyor
                   kullanici.Parola = kul.Parola.Trim();
               }
            }
            catch 
            {  
            }
           
            kullanici.Yetki = kul.Yetki;
            db.Kullanicilar.Update(kullanici);
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }

        public IActionResult KullaniciSil(int id)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var kullanici = db.Kullanicilar.Where(cd => cd.Silindi == false && cd.KullaniciId == id).FirstOrDefault();
            kullanici.Silindi = true;


            db.Kullanicilar.Update(kullanici);
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }

        public IActionResult Menuler()
        {//istediğimiz tabloyu çekicez 
            var menuler = db.Menuler.Where(s => s.Silindi == false).OrderBy(s => s.Baslik).ToList();
            //silinmeyen sayfaların başlığına göre sıralanarak gelsin
            return View(menuler);

        }
        public IActionResult MenuEkle()
        {
            var menuler = (from k in db.Menuler.Where(k => k.Silindi == false
                                                   && k.Aktif == true && k.UstId==null).ToList()
                           select new SelectListItem
                           {
                               Text = k.Baslik,
                               Value = k.MenuId.ToString()
                           }
                           );
            // üst menü seçmek zorunda değil boş olmasıda gerekiyor
            var m2 = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = " Yok",
                Value = "0"
                } 
            } ;
            //3. dereceden menüler olmayan
            ViewBag.ustmenuler = menuler.Union(m2).OrderBy(t => t.Text);
            //birleştirip sıraladık alfabetik olarak gözükecek diğer tarafta
            var sayfalar = (from k in db.Sayfalar.Where(k => k.Silindi == false
                                                   && k.Aktif == true ).OrderBy(s => s.Baslik).ToList()
                           select new SelectListItem
                           {
                               Text = k.Baslik,
                               Value = k.SayfaId.ToString()
                           }
                           );

            
            //sayfaları siraladik alıp
            //viewbag ile diğer tarafa göndericez
            ViewBag.sayfalar = sayfalar;
            return View();
        }
        [HttpPost]
        public IActionResult MenuEkle(Menuler m)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            if (m.UstId == 0)
            {//kaydetme işleminde bize null lazım 
                m.UstId = null;
                //anamenüye atanmış olacak
            }


            m.Silindi = false; //gelen modelin silindisi false olsun
            db.Menuler.Add(m);
            db.SaveChanges();
            return RedirectToAction("Menuler");
        }
        public IActionResult MenuGetir(int id)
        {
            var menuler = (from k in db.Menuler.Where(k => k.Silindi == false
                                                   && k.Aktif == true && k.UstId == null).ToList()
                           select new SelectListItem
                           {
                               Text = k.Baslik,
                               Value = k.MenuId.ToString()
                           }
                           );
            // üst menü seçmek zorunda değil boş olmasıda gerekiyor
            var m2 = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = " Yok",
                Value = "0"
                }
            };
            //3. dereceden menüler olmayan
            ViewBag.ustmenuler = menuler.Union(m2).OrderBy(t => t.Text);
            //birleştirip sıraladık alfabetik olarak gözükecek diğer tarafta
            var sayfalar = (from k in db.Sayfalar.Where(k => k.Silindi == false
                                                   && k.Aktif == true).OrderBy(s => s.Baslik).ToList()
                            select new SelectListItem
                            {
                                Text = k.Baslik,
                                Value = k.SayfaId.ToString()
                            }
                           );


            //sayfaları siraladik alıp
            //viewbag ile diğer tarafa göndericez
            ViewBag.sayfalar = sayfalar;

            var menu = db.Menuler.Where(s => s.Silindi == false && s.MenuId == id).FirstOrDefault();
            return View("MenuGuncelle", menu);
        }
        public IActionResult MenuGuncelle(Menuler mnu)
        {//sayfalar veritabanından geldiği için direk gönderebiliriizz
            var menu = db.Menuler.Where(s => s.Silindi == false && s.MenuId == mnu.MenuId).FirstOrDefault();
            menu.Baslik = mnu.Baslik;
            menu.Url = mnu.Url;
            menu.Aktif = mnu.Aktif;
            menu.Sira = mnu.Sira;
            if (mnu.UstId == 0)
            {//kaydetme işleminde bize null lazım 
                mnu.UstId = null;
                //anamenüye atanmış olacak
            }
            menu.UstId = mnu.UstId;
            db.Menuler.Update(menu);
            db.SaveChanges();
            return RedirectToAction("Menuler");
        }

        public IActionResult MenuSil(int id)
        {
            var menu = db.Menuler.Where(s => s.Silindi == false && s.MenuId == id).FirstOrDefault();
            menu.Silindi = true;

            db.Menuler.Update(menu);
            db.SaveChanges();
            return RedirectToAction("Menuler");
        }


        public IActionResult CikisYap()
        {
            return View();
        }

    }
}
