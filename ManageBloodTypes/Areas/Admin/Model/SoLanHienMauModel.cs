using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageBloodTypes.Areas.Admin.Model
{
    public class SoLanHienMauModel
    {
        public string HoTen { get; set; }        // Họ tên khách hàng
        public int? MaTaiKhoan { get; set; }   // Mã tài khoản
        public int? SoLanHienMau { get; set; }    // Số lần hiến máu
        public int? SoLanNhanMau { get; set; }
        public string NhomMau { get; set; }
    }
}