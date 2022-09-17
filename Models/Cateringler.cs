using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Catering.Models
{
    public partial class Cateringler
    {
        public Cateringler()
        {
            CateringDetail = new HashSet<CateringDetail>();
        }

        public int CateringId { get; set; }
        public string Cateringadi { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }

        public virtual ICollection<CateringDetail> CateringDetail { get; set; }
    }
}
