using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Catering.Models
{
    public partial class Menuler
    {
        public int MenuId { get; set; }
        public string Baslik { get; set; }
        public string Url { get; set; }
        public byte? Sira { get; set; }
        public int? UstId { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }
    }
}
