using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ManageBloodTypes.Areas.Admin.Model;
using ManageBloodTypes.DBContext;

namespace ManageBloodTypes.Areas.Admin.Controllers
{
    public class AThongKeMausController : Controller
    {
        private QLMauEntities db = new QLMauEntities();
        public ActionResult Index()
        {
            var topDonors = db.tbThongKeMaus
                           .Where(x => (bool)!x.Hide) // Loại bỏ các dòng có Hide = true
                           .OrderByDescending(x => x.SoLanHienMau)
                           .Take(5)
                           .Join(
                               db.tbThongTinCaNhans, // Bảng chứa thông tin cá nhân
                               tk => tk.MaTaiKhoan, // Dữ liệu từ bảng tbThongKeMau
                               tt => tt.MaTaiKhoan, // Dữ liệu từ bảng tbThongTinCaNhan
                               (tk, tt) => new SoLanHienMauModel
                               {
                                   MaTaiKhoan = tk.MaTaiKhoan,
                                   SoLanHienMau = tk.SoLanHienMau,
                                   HoTen = tt.HoTen
                               }
                           )
                           .ToList();

            return View(topDonors);
        }
        
        public ActionResult ThongKeNhomMau()
        {
            // Lấy dữ liệu từ bảng tbLichSuGiaoDich, lọc theo IDNhomMau và TinhTrangYeuCau
            var data = db.tbLichSuGiaoDiches
                .Where(x => x.TinhTrangYeuCau == true || x.TinhTrangYeuCau == false) // Lọc tất cả giao dịch hiến máu và nhận máu
                .GroupBy(x => x.IDNhomMau)
                .Select(g => new
                {
                    IDNhomMau = g.Key,
                    TenNhomMau = db.tbNhomMaus.Where(nm => nm.IDNhomMau == g.Key).Select(nm => nm.TenNhomMau).FirstOrDefault(),
                    SoLanHienMau = g.Count(x => x.TinhTrangYeuCau == true), // Đếm số lần hiến máu
                    SoLanNhanMau = g.Count(x => x.TinhTrangYeuCau == false) // Đếm số lần nhận máu
                })
                .ToList();

            // Map lại dữ liệu thành các đối tượng SoLanHienMauModel
            var result = data.Select(x => new SoLanHienMauModel
            {
                NhomMau = x.TenNhomMau,
                SoLanHienMau = x.SoLanHienMau,
                SoLanNhanMau = x.SoLanNhanMau
            }).ToList();

            // Truyền dữ liệu vào View
            return View(result);
        }

    }
}
