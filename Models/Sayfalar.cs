using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Catering.Models
{
    public partial class Sayfalar
    {
        public int SayfaId { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }
    }
}
