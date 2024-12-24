using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageBloodTypes.Models
{
    public class HinhAnhModel
    {
        public int IDHinh { get; set; }
        public string HinhAnh { get; set; }
        public string TieuDe { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}