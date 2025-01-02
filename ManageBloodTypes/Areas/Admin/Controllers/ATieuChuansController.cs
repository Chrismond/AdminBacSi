using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManageBloodTypes.App_Start;
using ManageBloodTypes.DBContext;

namespace ManageBloodTypes.Areas.Admin.Controllers
{
    public class ATieuChuansController : Controller
    {
        private QLMauEntities db = new QLMauEntities();
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/TieuChuan/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
        // GET: Admin/ATieuChuans
        public ActionResult Index()
        {
            return View(db.tbTieuChuans.ToList());
        }

        // GET: Admin/ATieuChuans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTieuChuan tbTieuChuan = db.tbTieuChuans.Find(id);
            if (tbTieuChuan == null)
            {
                return HttpNotFound();
            }
            return View(tbTieuChuan);
        }

        // GET: Admin/ATieuChuans/Create
        public ActionResult Create()
        {
            tbTieuChuan item = new tbTieuChuan();
            return View(item);
        }

        // POST: Admin/ATieuChuans/Create
       
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbTieuChuan tbTieuChuan, HttpPostedFileBase file)
        {
            tbTieuChuan item = new tbTieuChuan();
            try
            {
                if (file != null)
                {
                    item.Icon = UploadImage(file);
                }
                else
                {
                    item.Icon = tbTieuChuan.Icon;
                }

                item.TieuDe = tbTieuChuan.TieuDe;
                item.NoiDung = tbTieuChuan.NoiDung;
                item.Hide = false;
                db.tbTieuChuans.Add(item);
                db.SaveChanges();
                return Redirect("/admin/ATieuChuans");
            }
            catch
            {
                return View(tbTieuChuan);
            }
        }

        // GET: Admin/ATieuChuans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTieuChuan tbTieuChuan = db.tbTieuChuans.Find(id);
            if (tbTieuChuan == null)
            {
                return HttpNotFound();
            }
            return View(tbTieuChuan);
        }

        // POST: Admin/ATieuChuans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbTieuChuan tbTieuChuan, HttpPostedFileBase Editfile)
        {
            tbTieuChuan item = new tbTieuChuan();
            try
            {
                item = db.tbTieuChuans.Find(tbTieuChuan.IDTieuChuan);
                if (Editfile != null)
                {
                    item.Icon = UploadImage(Editfile);
                }
                item.TieuDe = tbTieuChuan.TieuDe;
                item.NoiDung = tbTieuChuan.NoiDung;
                item.Hide = tbTieuChuan.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/ATieuChuans");
            }
            catch (Exception ex)
            {
                return View(tbTieuChuan);
            }
        }

        // GET: Admin/ATieuChuans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTieuChuan tbTieuChuan = db.tbTieuChuans.Find(id);
            if (tbTieuChuan == null)
            {
                return HttpNotFound();
            }
            return View(tbTieuChuan);
        }

        // POST: Admin/ATieuChuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTieuChuan tbTieuChuan = db.tbTieuChuans.Find(id);
            db.tbTieuChuans.Remove(tbTieuChuan);
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
