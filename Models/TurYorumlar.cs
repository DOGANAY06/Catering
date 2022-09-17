using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catering.Models
{
    public class TurYorumlar
    {//turler ve yorumları birleştiricez 
        public CateringDetail cateringDetail { get; set; }
        public List<Yorum> yorumlars { get; set; }

    }
}
