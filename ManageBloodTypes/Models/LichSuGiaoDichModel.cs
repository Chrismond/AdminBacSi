using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageBloodTypes.Models
{
    public class LichSuGiaoDichModel
    {
        public int? MaTaiKhoan { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string Gmail { get; set; }
        public string DiaChi { get; set; }
        public Nullable<int> IDPhuong { get; set; }
        public Nullable<int> IDQuan { get; set; }
        public Nullable<int> IDThanhPho { get; set; }
        public int IDGiaoDich { get; set; }
        public Nullable<int> IDNhomMau { get; set; }
        public Nullable<bool> TinhTrangYeuCau { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public Nullable<System.DateTime> NgayYeuCau { get; set; }
        public int? SoLuongMau { get; set; }
        public Nullable<System.DateTime> NgayXacNhan { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}