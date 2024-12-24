using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageBloodTypes.DBContext;
using ManageBloodTypes.Models;


namespace ManageBloodTypes.Controllers
{
    public class HomeController : Controller
    {
        private QLMauEntities db = new QLMauEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Gmail, string MatKhau)
        {
            // Kiểm tra nếu người dùng bỏ trống thông tin
            if (string.IsNullOrWhiteSpace(Gmail) || string.IsNullOrWhiteSpace(MatKhau))
            {
                if (string.IsNullOrWhiteSpace(Gmail))
                {
                    ViewBag.EmailError = "Vui lòng nhập email.";
                }

                if (string.IsNullOrWhiteSpace(MatKhau))
                {
                    ViewBag.PasswordError = "Vui lòng nhập mật khẩu.";
                }

                return View();
            }

            // Kiểm tra email trong cơ sở dữ liệu
            var user = db.tbThongTinCaNhans.FirstOrDefault(u => u.Gmail == Gmail);

            if (user == null)
            {
                // Email không tồn tại
                ViewBag.EmailError = "Email không tồn tại.";
                return View();
            }

            // Kiểm tra mật khẩu và id
            if (user.MatKhau != MatKhau)
            {
                ViewBag.PasswordError = "Mật khẩu không đúng.";
                return View();
            }
            if (Gmail.Trim().ToLower() == "bacsi123@gmail.com" && MatKhau.Trim() == "Bacsi123")
            {
                Session["HinhAnh"] = user.HinhAnh;
                Session["UserEmail"] = user.Gmail;
                Session["MaTaiKhoan"] = user.MaTaiKhoan;
                Session["HoTen"] = user.HoTen;
                return RedirectToAction("Index", "Doctor");
            }
            if (Gmail.Trim().ToLower() == "admin123@gmail.com" && MatKhau.Trim() == "Admin123")
            {
                Session["HinhAnh"] = user.HinhAnh;
                Session["UserEmail"] = user.Gmail;
                Session["MaTaiKhoan"] = user.MaTaiKhoan;
                Session["HoTen"] = user.HoTen;
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            Session["UserEmail"] = user.Gmail;
            Session["HoTen"] = user.HoTen;
            // Đăng nhập thành công, chuyển hướng đến Dashboard
            return RedirectToAction("Home", "HomePage");
        }

        public ActionResult Logout()
        {
            Session.Clear(); // Xóa session
            return RedirectToAction("Index", "Home"); // Chuyển hướng
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}