using Catering.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Catering.Controllers
{
    public class GirisController : Controller
    {

        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GirisYap(Kullanicilar k,string ReturnUrl )
        {//giriş yap buttonuna baştıktan sonra bu devreye giricek  returnurl sabit isim gidilecek sayfanın urforthl si tutulaca
            cateringserviceDBContext db = new cateringserviceDBContext();
            var kullanici = db.Kullanicilar.FirstOrDefault(kul => kul.Eposta == k.Eposta && kul.Parola ==k.Parola && kul.Silindi == false &&  kul.Aktif == true);
            if (kullanici!=null)
            {
                string yetki =(bool)kullanici.Yetki ? "Yonetici" : "Uye";

                var talepler = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,kullanici.Eposta?.ToString()),
                    new Claim(ClaimTypes.Role,yetki),
                    new Claim(ClaimTypes.NameIdentifier,kullanici.KullaniciId.ToString()) //bunu bilgerim kısmında tutmak içim sakladık
                };
                ClaimsIdentity kimlik = new ClaimsIdentity(talepler,"Login");
                ClaimsPrincipal kural = new ClaimsPrincipal(kimlik);

                await HttpContext.SignInAsync(kural);
                if (!String.IsNullOrEmpty(ReturnUrl))
                {//şifreli bir sayfaya girmek istemizse 
                    return Redirect(ReturnUrl);
                }
                else
                {//yetkiliyse yonetim uyeyse başka sayfaya 
                    if ((bool)kullanici.Yetki)
                    {//admin rolündeyse
                        return Redirect("/Yonetim/Index");
                    }
                    else
                    {//normal üyeyse
                        return Redirect("/Home/Index");
                    }

                }
            }
            return View();
        }
        cateringserviceDBContext db = new cateringserviceDBContext();
        public IActionResult KaydolPanel()
        {
            return View();
        }


        [HttpPost]
        public IActionResult KaydolPanel(Kullanicilar k)
        {
            k.Silindi = false; //gelen modelin silindisi false olsun
            db.Kullanicilar.Add(k);
            db.SaveChanges();
            return RedirectToAction("GirisYap");
        }
        public async Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home"); //çıkış yapıldısa home ındexe gidilsin
        }
    }
}
