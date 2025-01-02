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
    public class ABannersController : Controller
    {
        private QLMauEntities db = new QLMauEntities();
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/Banner/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
        // GET: Admin/ABanners
        public ActionResult Index()
        {
            return View(db.tbBanners.ToList());
        }

        // GET: Admin/ABanners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanner tbBanner = db.tbBanners.Find(id);
            if (tbBanner == null)
            {
                return HttpNotFound();
            }
            return View(tbBanner);
        }

        // GET: Admin/ABanners/Create
        public ActionResult Create()
        {
            tbBanner item = new tbBanner();
            return View(item);
        }

        // POST: Admin/ABanners/Create
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbBanner tbBanner, HttpPostedFileBase file)
        {
            tbBanner item = new tbBanner();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);
                }
                else
                {
                    item.HinhAnh = tbBanner.HinhAnh;
                }

                item.TieuDe = tbBanner.TieuDe;
                item.NoiDung = tbBanner.NoiDung;
                item.Hide = false;
                db.tbBanners.Add(item);
                db.SaveChanges();
                return Redirect("/admin/ABanners");
            }
            catch
            {
                return View(tbBanner);
            }
        }

        // GET: Admin/ABanners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanner tbBanner = db.tbBanners.Find(id);
            if (tbBanner == null)
            {
                return HttpNotFound();
            }
            return View(tbBanner);
        }

        // POST: Admin/ABanners/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbBanner tbBanner, HttpPostedFileBase Editfile)
        {
            tbBanner item = new tbBanner();
            try
            {
                item = db.tbBanners.Find(tbBanner.IDBanner);
                if (Editfile != null)
                {
                    item.HinhAnh = UploadImage(Editfile);
                }
                item.TieuDe = tbBanner.TieuDe;
                item.NoiDung = tbBanner.NoiDung;
                item.Hide = tbBanner.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/ABanners");
            }
            catch 
            {
                return View(tbBanner);
            }
        }

        // GET: Admin/ABanners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanner tbBanner = db.tbBanners.Find(id);
            if (tbBanner == null)
            {
                return HttpNotFound();
            }
            return View(tbBanner);
        }

        // POST: Admin/ABanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBanner tbBanner = db.tbBanners.Find(id);
            db.tbBanners.Remove(tbBanner);
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
