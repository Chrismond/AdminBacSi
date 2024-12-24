using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageBloodTypes.Models
{
    public class BannerModel
    {
        public int IDBanner { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}