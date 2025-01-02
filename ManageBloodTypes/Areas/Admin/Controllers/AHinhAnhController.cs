using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ManageBloodTypes.App_Start;
using ManageBloodTypes.DBContext;

namespace ManageBloodTypes.Areas.Admin.Controllers
{
    public class AHinhAnhController : Controller
    {
        private QLMauEntities db = new QLMauEntities();
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/HinhAnh/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
        private string fileName = "";
        public string UploadImage(HttpPostedFileBase upload)
        {
            fileName = Path.GetFileName(upload.FileName);
            fileName = Processing.UrlImages(fileName);
            bool exsits = System.IO.Directory.Exists(Server.MapPath(pathFile));
            if (!exsits)
                System.IO.Directory.CreateDirectory(Server.MapPath(pathFile));
            var path = Path.Combine(Server.MapPath(pathFile), fileName);
            upload.SaveAs(path);
            return pathFile + fileName;
        }
        #endregion

        #endregion
        // GET: Admin/AHinhAnh
        public ActionResult Index()
        {
            return View(db.tbHinhAnhs.ToList());
        }

        // GET: Admin/AHinhAnh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHinhAnh tbHinhAnh = db.tbHinhAnhs.Find(id);
            if (tbHinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(tbHinhAnh);
        }

        // GET: Admin/AHinhAnh/Create
        public ActionResult Create()
        {
            tbHinhAnh item = new tbHinhAnh();
            return View(item);
        }

        // POST: Admin/AHinhAnh/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbHinhAnh tbHinhAnh, HttpPostedFileBase file)
        {
            tbHinhAnh item = new tbHinhAnh();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);
                }
                else
                {
                    item.HinhAnh = tbHinhAnh.HinhAnh;
                }

                item.TieuDe = tbHinhAnh.TieuDe;
                item.Hide = false;
                db.tbHinhAnhs.Add(item);
                db.SaveChanges();
                return Redirect("/admin/AHinhAnh");
            }
            catch
            {
                return View(tbHinhAnh);
            }
        }

        // GET: Admin/AHinhAnh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHinhAnh tbHinhAnh = db.tbHinhAnhs.Find(id);
            if (tbHinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(tbHinhAnh);
        }

        // POST: Admin/AHinhAnh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbHinhAnh tbHinhAnh, HttpPostedFileBase Editfile)
        {
            try
            {
                tbHinhAnh item = new tbHinhAnh();

                item = db.tbHinhAnhs.Find(tbHinhAnh.IDHinh);
                if (Editfile != null)
                {
                    item.HinhAnh = UploadImage(Editfile);
                }
                item.TieuDe = tbHinhAnh.TieuDe;
                item.Hide = tbHinhAnh.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/AHinhAnh");
            }
            catch 
            {
                return View(tbHinhAnh);
            }
        }

        // GET: Admin/AHinhAnh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHinhAnh tbHinhAnh = db.tbHinhAnhs.Find(id);
            if (tbHinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(tbHinhAnh);
        }

        // POST: Admin/AHinhAnh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbHinhAnh tbHinhAnh = db.tbHinhAnhs.Find(id);
            db.tbHinhAnhs.Remove(tbHinhAnh);
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
