using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Catering.Models
{
    public partial class CateringDetail
    {
        public CateringDetail()
        {
            Yorum = new HashSet<Yorum>();
        }

        public int TurId { get; set; }
        public string Cateringfirmabilgi { get; set; }
        public string Icerik { get; set; }
        public byte? Sira { get; set; }
        public int CateringId { get; set; }
        public DateTime? Eklemetarihi { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }

        public virtual Cateringler Catering { get; set; }
        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}
