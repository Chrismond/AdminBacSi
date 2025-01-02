using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManageBloodTypes.Areas.Admin.Model;
using ManageBloodTypes.DBContext;
using ManageBloodTypes.Models;

namespace ManageBloodTypes.Areas.Admin.Controllers
{
    public class ALichSuGiaoDichesController : Controller
    {
        private QLMauEntities db = new QLMauEntities();
        public string GetMaTaiKhoan1(int? id)
        {
            string html = "";

            List<TinhTrangGiaoDichModel> lst = new List<TinhTrangGiaoDichModel>();
            lst = (from grpo in db.tbThongTinCaNhans
                   where grpo.Hide != true
                   select (new TinhTrangGiaoDichModel
                   {
                       Id = grpo.MaTaiKhoan,
                       Name = grpo.MaTaiKhoan,
                   })).ToList();
            int tong = lst.Count;

            if (id != null)
            {
                html = "<option value= ''> ----- Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].Id)
                    {
                        html += "<option selected value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";

                    }
                }
            }
            else
            {
                html = "<option selected value= ''> ----- Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                }
            }
            return html;

        }


        public string GetMaTaiKhoan(int? id)
        {
            string html = "<option value= ''> ----- Chọn -----</option>";

            var lst = db.tbThongTinCaNhans
                         .Where(grpo => grpo.Hide != true)
                         .Select(grpo => new { grpo.IDThongTin, grpo.MaTaiKhoan })
                         .ToList();

            foreach (var item in lst)
            {
                html += id == item.IDThongTin
                    ? $"<option selected value='{item.MaTaiKhoan}'>{item.MaTaiKhoan}</option>"
                    : $"<option value='{item.MaTaiKhoan}'>{item.MaTaiKhoan}</option>";
            }

            return html;
        }
        public string GetNhomMau(int? id)
        {
            string html = "";

            List<NhomMauModel> lst = new List<NhomMauModel>();
            lst = (from grpo in db.tbNhomMaus
                   where grpo.Hide != true
                   select (new NhomMauModel
                   {
                       Id = grpo.IDNhomMau,
                       Name = grpo.TenNhomMau

                   })).ToList();
            int tong = lst.Count;

            if (id != null)
            {
                html = "<option value= ''> ----- Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].Id)
                    {
                        html += "<option selected value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";

                    }
                }
            }
            else
            {
                html = "<option selected value= ''> ----- Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                }
            }
            return html;

        }
        public String GetNhomMauName(int id)

        {
            try
            {
                return db.tbNhomMaus.Find(id).TenNhomMau;
            }
            catch
            {
                return "";
            }
        }
        // GET: Admin/ALichSuGiaoDiches
        public ActionResult Index()
        {
            return View(db.tbLichSuGiaoDiches.ToList());
        }

        // GET: Admin/ALichSuGiaoDiches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLichSuGiaoDich tbLichSuGiaoDich = db.tbLichSuGiaoDiches.Find(id);
            if (tbLichSuGiaoDich == null)
            {
                return HttpNotFound();
            }
            return View(tbLichSuGiaoDich);
        }
        public JsonResult GetNhomMauByMaTaiKhoan(int? maTaiKhoan)
        {
            if (!maTaiKhoan.HasValue)
            {
                return Json(new { success = false, message = "Mã tài khoản không hợp lệ" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var thongTin = db.tbThongTinCaNhans
                                 .Where(x => x.MaTaiKhoan == maTaiKhoan && x.Hide != true)
                                 .Select(x => new { x.IDNhomMau })
                                 .FirstOrDefault();

                if (thongTin != null && thongTin.IDNhomMau.HasValue)
                {
                    return Json(new { success = true, idNhomMau = thongTin.IDNhomMau.Value }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, idNhomMau = "Không có thông tin" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetNhomMauNameById(int? idNhomMau)
        {
            if (!idNhomMau.HasValue)
            {
                return Json(new { success = false, message = "ID Nhóm Máu không hợp lệ" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var nhomMau = db.tbNhomMaus
                                .Where(x => x.IDNhomMau == idNhomMau)
                                .Select(x => x.TenNhomMau)
                                .FirstOrDefault();

                if (!string.IsNullOrEmpty(nhomMau))
                {
                    return Json(new { success = true, tenNhomMau = nhomMau }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, tenNhomMau = "Không có thông tin" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/ALichSuGiaoDiches/Create
        public ActionResult Create()
        {
            tbLichSuGiaoDich item = new tbLichSuGiaoDich();
            string maTaiKhoanHtml = GetMaTaiKhoan(null);

            // Gán HTML danh sách tài khoản vào ViewBag
            ViewBag.MaTaiKhoanHtml = maTaiKhoanHtml;
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbLichSuGiaoDich tbLic)
        {
            tbLichSuGiaoDich item = new tbLichSuGiaoDich();
            try
            {
                var thongTinTaiKhoan = db.tbThongTinCaNhans
                        .FirstOrDefault(x => x.MaTaiKhoan == tbLic.MaTaiKhoan);
                item.MaTaiKhoan = tbLic.MaTaiKhoan;
                item.IDNhomMau = thongTinTaiKhoan.IDNhomMau;
                item.TinhTrangYeuCau = tbLic.TinhTrangYeuCau;
                item.TrangThai = tbLic.TrangThai;
                item.NgayYeuCau = tbLic.NgayYeuCau;
                item.SoLuongMau = tbLic.SoLuongMau;
                item.NgayXacNhan = tbLic.NgayXacNhan;

                item.Hide = false;
                db.tbLichSuGiaoDiches.Add(item);
                db.SaveChanges();
                return Redirect("/Admin/ALichSuGiaoDiches");
            }
            catch
            {
                ViewBag.DanhSachTaiKhoan = db.tbThongTinCaNhans
            .Select(x => new
            {
                x.MaTaiKhoan,
                x.IDNhomMau
            })
            .ToList();

                return View(tbLic);
            }
        }

        // GET: Admin/ALichSuGiaoDiches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLichSuGiaoDich tbLichSuGiaoDich = db.tbLichSuGiaoDiches.Find(id);
            if (tbLichSuGiaoDich == null)
            {
                return HttpNotFound();
            }
            ViewBag.DanhSachTaiKhoan = db.tbThongTinCaNhans.Where(x => x.Hide != true).ToList();
            return View(tbLichSuGiaoDich);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbLichSuGiaoDich tbLic)
        {
            try
            {
                tbLichSuGiaoDich item = new tbLichSuGiaoDich();

                item = db.tbLichSuGiaoDiches.Find(tbLic.IDGiaoDich);

                item.MaTaiKhoan = tbLic.MaTaiKhoan;
                item.IDNhomMau = tbLic.IDNhomMau;
                item.TinhTrangYeuCau = tbLic.TinhTrangYeuCau;
                item.TrangThai = tbLic.TrangThai;
                item.NgayYeuCau = tbLic.NgayYeuCau;
                item.SoLuongMau = tbLic.SoLuongMau;
                item.NgayXacNhan = tbLic.NgayXacNhan;
                item.Hide = tbLic.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/ALichSuGiaoDiches");
            }
            catch
            {
                return View(tbLic);
            }
        }

        // GET: Admin/ALichSuGiaoDiches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLichSuGiaoDich tbLichSuGiaoDich = db.tbLichSuGiaoDiches.Find(id);
            if (tbLichSuGiaoDich == null)
            {
                return HttpNotFound();
            }
            return View(tbLichSuGiaoDich);
        }

        // POST: Admin/ALichSuGiaoDiches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbLichSuGiaoDich tbLichSuGiaoDich = db.tbLichSuGiaoDiches.Find(id);
            db.tbLichSuGiaoDiches.Remove(tbLichSuGiaoDich);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
