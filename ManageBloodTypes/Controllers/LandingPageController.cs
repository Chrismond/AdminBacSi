using ManageBloodTypes.Areas.Admin.Model;
using ManageBloodTypes.DBContext;
using ManageBloodTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageBloodTypes.Controllers
{
    public class LandingPageController : Controller
    {
        QLMauEntities db = new QLMauEntities();
        // GET: LandingPage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Banner()
        {
            List<BannerModel> banners = new List<BannerModel>();
            banners = (from ca in db.tbBanners
                       where ca.Hide == false
                       select (new BannerModel
                       {
                           IDBanner = ca.IDBanner,
                           TieuDe = ca.TieuDe,
                           NoiDung = ca.NoiDung,
                           HinhAnh = ca.HinhAnh,
                           Hide = ca.Hide,
                       })).ToList();
            return PartialView(banners);
        }
        public ActionResult TinTuc()
        {
            List<ThongTinMauModel> news = new List<ThongTinMauModel>();
            news = (from ca in db.tbBloodInfors
                    where ca.Hide == false
                    select (new ThongTinMauModel
                    {
                        IDThTinMau = ca.IDThTinMau,
                        TieuDe = ca.TieuDe,
                        NoiDung = ca.NoiDung,
                        HinhAnh = ca.HinhAnh,
                        Hide = ca.Hide,
                    })).ToList();
            return PartialView(news);
        }
        public ActionResult HinhAnh()
        {
            List<HinhAnhModel> news = new List<HinhAnhModel>();
            news = (from ca in db.tbHinhAnhs
                    where ca.Hide == false
                    select (new HinhAnhModel
                    {
                        IDHinh = ca.IDHinh,
                        TieuDe = ca.TieuDe,
                        HinhAnh = ca.HinhAnh,
                        Hide = ca.Hide,
                    })).ToList();
            return PartialView(news);
        }
        public ActionResult TieuChuan()
        {
            List<TieuChuanModel> a = new List<TieuChuanModel>();
            a = (from ca in db.tbTieuChuans
                 where ca.Hide == false
                 select (new TieuChuanModel
                 {
                     IDTieuChuan = ca.IDTieuChuan,
                     Icon = ca.Icon,
                     TieuDe = ca.TieuDe,
                     NoiDung = ca.NoiDung,
                     Hide = ca.Hide,
                 })).ToList();
            return PartialView(a);
        }
        public ActionResult Details(int? id)
        {
            var article = db.tbBloodInfors.FirstOrDefault(a => a.IDThTinMau == id);

            if (article == null)
            {
                return HttpNotFound();
            }

            var otherArticles = db.tbBloodInfors
                                  .Where(a => a.IDThTinMau != id)
                                  .Take(5)
                                  .ToList();

            var otherArticlesModels = otherArticles.Select(a => new ThongTinMauModel
            {
                IDThTinMau = a.IDThTinMau,
                TieuDe = a.TieuDe,
                HinhAnh = a.HinhAnh,
                NoiDung = a.NoiDung
            }).ToList();

            var model = new ThongTinMauModel
            {
                IDThTinMau = article.IDThTinMau,
                TieuDe = article.TieuDe,
                NoiDung = article.NoiDung,
                HinhAnh = article.HinhAnh,
                OtherArticles = otherArticlesModels
            };

            return View(model);
        }
        public ActionResult TuyenDuong()
        {
            List<TuyenDuongModel> topDonors =
                (from tk in db.tbThongKeMaus
                 join tt in db.tbThongTinCaNhans
                 on tk.MaTaiKhoan equals tt.MaTaiKhoan
                 where tk.Hide == false
                 orderby tk.SoLanHienMau descending
                 select new TuyenDuongModel
                 {
                     HinhAnh = tt.HinhAnh,
                     MaTaiKhoan = tk.MaTaiKhoan,
                     SoLanHienMau = tk.SoLanHienMau,
                     HoTen = tt.HoTen
                 }).Take(5).ToList();
            return PartialView(topDonors);
        }
        public ActionResult ThongKe_HomePgae()
        {
            List<int> ints = new List<int>();   
            NghiepVuModel model = new NghiepVuModel();
            model.SoNguoi = db.tbThongTinCaNhans.Where(s => s.Hide == false).Count();
            ints = db.tbThongKeMaus.Where(x=>x.Hide == false).Select(s=>s.SoLanHienMau.Value).ToList();
            model.SLHienMau = ints.Sum();
            ints = new List<int>();
            ints = db.tbThongKeMaus.Where(x => x.Hide == false).Select(s => s.SoLanNhanMau.Value).ToList();
            model.SLNhanMau = ints.Sum();
            ints = new List<int>();
            ints = db.tbLichSuGiaoDiches.Where(x => x.Hide == false && x.TinhTrangYeuCau == false && x.TrangThai == false).Select(s => s.IDGiaoDich).ToList();
            model.SlCho = ints.Count;
            return PartialView(model);
        }
        public ActionResult YeuCau()
        {
            // Kết hợp 3 bảng để lấy dữ liệu
            var a = (from ca in db.tbThongKeMaus
                     join info in db.tbThongTinCaNhans
                     on ca.MaTaiKhoan equals info.MaTaiKhoan // Liên kết qua MaTaiKhoan
                     join nhom in db.tbNhomMaus
                     on info.IDNhomMau equals nhom.IDNhomMau // Liên kết qua IDNhomMau
                     where ca.Hide == false                 // Điều kiện lọc
                     select new YeuCauModel
                     {
                         ID = ca.IDThongKeMau,         // ID thống kê máu
                         Ten = nhom.TenNhomMau,       // Tên nhóm máu từ bảng tbNhomMaus
                         SoLanHienMau = ca.SoLanHienMau,    // Số lần hiến máu
                         SoLanNhanMau = ca.SoLanNhanMau,    // Số lần nhận máu
                     }).ToList();

            // Trả về PartialView với danh sách dữ liệu
            return PartialView(a);
        }



    }

}
