using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Catering.Models
{
    public partial class Yorum
    {
        public int YorumId { get; set; }
        public string Yorum1 { get; set; }
       
        public DateTime? Eklemetarihi { get; set; }
        public int TurId { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }
        public int UyeId { get; set; }

        public virtual CateringDetail Tur { get; set; }
        public virtual Kullanicilar Uye { get; set; }
    }
}
