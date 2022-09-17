using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Catering.Models
{
    public partial class Kullanicilar
    {
        public Kullanicilar()
        {
            Yorum = new HashSet<Yorum>();
        }

        public int KullaniciId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public string Parola { get; set; }
        public bool? Yetki { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }

        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}
