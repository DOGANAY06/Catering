using Catering.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Catering.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        cateringserviceDBContext db = new cateringserviceDBContext();
        public IActionResult Index(int id)
        {
            
            var sayfa = db.Sayfalar.Find(id);
            //var sayfa = db.Sayfalar.Where(a => a.Silindi == false && a.Aktif == true && a.SayfaId == id).FirstOrDefault();
            return View(sayfa);
        }
        public IActionResult TumTurler()
        {
            
            var turler = db.CateringDetail.Include(k=>k.Catering).Where(a => a.Silindi == false && a.Aktif == true).OrderBy(s=>s.Sira).ToList();
            //var sayfa = db.Sayfalar.Where(a => a.Silindi == false && a.Aktif == true && a.SayfaId == id).FirstOrDefault();
            return View(turler);
        }
        public IActionResult Turler(int id)
        {
            TurYorumlar t = new TurYorumlar();
            //iki özelliği de atadık buna 
            var turler = db.CateringDetail.Include(k => k.Catering).Where(a => a.Silindi == false && a.Aktif == true && a.TurId == id).FirstOrDefault();
            //var sayfa = db.Sayfalar.Where(a => a.Silindi == false && a.Aktif == true && a.SayfaId == id).FirstOrDefault();
            t.cateringDetail = turler;
            var yorumlar = db.Yorum.Include(u=>u.Uye).Where(a => a.Silindi == false && a.Aktif == true && a.TurId == id)
                .OrderByDescending(y=>y.Eklemetarihi).ToList(); //yeniden eskiye sıraladım
            //bu tur için yapılmış olan bütün yorumlar gelecek
            t.yorumlars = yorumlar;
            return View(t);
        }
        public IActionResult YorumYap(Yorum yor)
        {//yorum yapılması
            yor.Silindi = false;
            yor.Aktif = false;
            yor.Eklemetarihi = DateTime.Now;
            db.Yorum.Add(yor);
            db.SaveChanges();
            TempData["mesaj"] = "Yorumunuz alındı, yönetici onayından sonra görüncecektir ";
            //yorum kabul edildiğine dair görüntü 
            return Redirect("/Home/Turler/" + yor.TurId);
            //yorumu yaptığımız yere aynen gitsin
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
