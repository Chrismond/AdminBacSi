using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageBloodTypes.Models
{
    public class ThongTinMauModel
    {
        public int IDThTinMau { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<bool> Hide { get; set; }
        public List<ThongTinMauModel> OtherArticles { get; set; }

    }
}