using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GGM.Models.DataContext;
using GGM.Models.Model;

namespace GGM.Controllers
{
    public class HizmetController : Controller
    {
        private dataContext db = new dataContext();

        // GET: Hizmet
        public ActionResult Index()
        {
            return View(db.Hizmet.ToList());
        }

        
       

        // GET: Hizmet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hizmet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Hizmet hizmet, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
            {
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                string hizmetimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(500, 500);
                img.Save("~/Upload/Hizmet/" + hizmetimgname);

                hizmet.ResimURL = "/Upload/Hizmet/" + hizmetimgname;
            }

            db.Hizmet.Add(hizmet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(hizmet);
        }

        [ValidateInput(false)]
        // GET: Hizmet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek Hizmet Bulunamadı";
            }
            var hizmet = db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();
            }

            return View(hizmet);
        }

        // POST: Hizmet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Edit(int? id, Hizmet hizmet, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var h = db.Hizmet.Where(x => x.HizmetId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string hizmetimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Upload/Hizmet/" + hizmetimgname);

                    h.ResimURL = "/Upload/Hizmet/" + hizmetimgname;
                }

                h.MevzuatCalismasi = hizmet.MevzuatCalismasi;
                h.DestekHizmetleri = hizmet.DestekHizmetleri;
                h.DigerHizmetler = hizmet.DigerHizmetler;
                h.GumrukIsleri = hizmet.GumrukIsleri;
                h.IhracatIslemleri = hizmet.IhracatIslemleri;
                h.IthalatIslemleri = hizmet.IthalatIslemleri;
                
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }


        // GET: Hizmet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmet hizmet = db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();
            }
            return View(hizmet);
        }

        // POST: Hizmet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hizmet hizmet = db.Hizmet.Find(id);
            db.Hizmet.Remove(hizmet);
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
