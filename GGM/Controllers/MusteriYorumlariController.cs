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
    public class MusteriYorumlariController : Controller
    {
        private dataContext db = new dataContext();

        // GET: MusteriYorumlari
        public ActionResult Index()
        {
            return View(db.MusteriYorumlari.ToList());
        }

        // GET: MusteriYorumlari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusteriYorumlari musteriYorumlari = db.MusteriYorumlari.Find(id);
            if (musteriYorumlari == null)
            {
                return HttpNotFound();
            }
            return View(musteriYorumlari);
        }

        // GET: MusteriYorumlari/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MusteriYorumlari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "MusteriYorumlariId,MusteriYorum,MusteriAd,ResimURL,Link")] MusteriYorumlari musteriYorumlari, HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {

                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                string musteriyorumlariname = Guid.NewGuid().ToString() + imginfo.Extension; // isimlendirme.
                img.Resize(150, 150); //yükseklik genişlik.
                img.Save("~/Upload/MusteriYorumlari/" + musteriyorumlariname); // kayıt yeri.

                musteriYorumlari.ResimURL = "/Upload/MusteriYorumlari/" + musteriyorumlariname;

            }
            db.MusteriYorumlari.Add(musteriYorumlari);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET: MusteriYorumlari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusteriYorumlari musteriYorumlari = db.MusteriYorumlari.Find(id);
            if (musteriYorumlari == null)
            {
                return HttpNotFound();
            }
            return View(musteriYorumlari);
        }

        // POST: MusteriYorumlari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, MusteriYorumlari musteriYorumlari, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.MusteriYorumlari.Where(i => i.MusteriYorumlariId == id).SingleOrDefault();
                if (ResimURL != null)
                {

                    if (System.IO.File.Exists(Server.MapPath(b.ResimURL))) //Dosya var mı ? Yok mu ?
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL)); // Resim var ise Sil.
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string musteriyorumlariname = Guid.NewGuid().ToString() + imginfo.Extension; // isimlendirme.
                    img.Resize(200, 200); //yükseklik genişlik.
                    img.Save("~/Upload/MusteriYorumlari/" + musteriyorumlariname); // kayıt yeri.

                    b.ResimURL = "/Upload/MusteriYorumlari/" + musteriyorumlariname;
                }

                b.MusteriAd = musteriYorumlari.MusteriAd;
                b.MusteriYorum = musteriYorumlari.MusteriYorum;
                b.Link = musteriYorumlari.Link;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musteriYorumlari);


        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusteriYorumlari musteriYorumlari = db.MusteriYorumlari.Find(id);
            if (musteriYorumlari == null)
            {
                return HttpNotFound();
            }
            return View(musteriYorumlari);
        }

        // POST: MusteriYorumlari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MusteriYorumlari musteriYorumlari = db.MusteriYorumlari.Find(id);
            db.MusteriYorumlari.Remove(musteriYorumlari);
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
