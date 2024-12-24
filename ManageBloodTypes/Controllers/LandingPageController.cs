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
                    select (new HinhAnhModel
                    {
                        IDHinh = ca.IDHinh,
                        TieuDe = ca.TieuDe,
                        HinhAnh = ca.HinhAnh,
                        Hide = ca.Hide,
                    })).ToList();
            return PartialView(news);
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


    }
}