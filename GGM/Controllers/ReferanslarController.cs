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

namespace GGM.Content
{
    public class ReferanslarController : Controller
    {
        private dataContext db = new dataContext();

        // GET: Referanslar
        public ActionResult Index()
        {
            return View(db.Referanslar.ToList());
        }

        // GET: Referanslar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referanslar = db.Referanslar.Find(id);
            if (referanslar == null)
            {
                return HttpNotFound();
            }
            return View(referanslar);
        }

        // GET: Referanslar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Referanslar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Referanslar referanslar, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string referansimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(200, 200);
                    img.Save("~/Upload/Referans/" + referansimgname);

                    referanslar.ResimURL = "/Upload/Referans/" + referansimgname;
                }
                db.Referanslar.Add(referanslar);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(referanslar);
        }

        // GET: Referanslar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referanslar = db.Referanslar.Find(id);
            if (referanslar == null)
            {
                return HttpNotFound();
            }
            return View(referanslar);
        }

        // POST: Referanslar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReferansId,ReferansAd,ResimURL")] Referanslar referanslar, HttpPostedFileBase ResimURL,int id)
        {
            if (ModelState.IsValid)
            {
                var s = db.Referanslar.Where(i => i.ReferansId == id).SingleOrDefault();
                if (ResimURL != null)
                {

                    if (System.IO.File.Exists(Server.MapPath(s.ResimURL))) //Dosya var mı ? Yok mu ?
                    {
                        System.IO.File.Delete(Server.MapPath(s.ResimURL)); // Resim var ise Sil.
                    }

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string referansimgname = Guid.NewGuid().ToString() + imginfo.Extension; // isimlendirme.
                    img.Resize(200, 200); //yükseklik genişlik.
                    img.Save("~/Upload/Referans/" + referansimgname); // kayıt yeri.

                    s.ResimURL = "/Upload/Referans/" + referansimgname;
                }

                //db.Entry(slider).State = EntityState.Modified; değişiklik algılanmadı  hatası
                s.ReferansAd = referanslar.ReferansAd;
               

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(referanslar);
        }

        // GET: Referanslar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referanslar = db.Referanslar.Find(id);
            if (referanslar == null)
            {
                return HttpNotFound();
            }
            return View(referanslar);
        }

        // POST: Referanslar/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Referanslar referanslar = db.Referanslar.Find(id);
        //    db.Referanslar.Remove(referanslar);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
