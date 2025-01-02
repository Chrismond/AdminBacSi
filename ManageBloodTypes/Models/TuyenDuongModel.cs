using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageBloodTypes.Models
{
    public class TuyenDuongModel
    {
        public int IDThongKeMau { get; set; }
        public Nullable<int> MaTaiKhoan { get; set; }
        public Nullable<int> SoLanHienMau { get; set; }
        public Nullable<int> SoLanNhanMau { get; set; }
        public Nullable<bool> Hide { get; set; }
        public string HoTen {  get; set; }

        public string HinhAnh {  get; set; }
    }
}