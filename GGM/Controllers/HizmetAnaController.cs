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
    public class HizmetAnaController : Controller
    {
        private dataContext db = new dataContext();

        // GET: HizmetAna
        public ActionResult Index()
        {
            return View(db.HizmetAna.ToList());
        }

        // GET: HizmetAna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HizmetAna hizmetAna = db.HizmetAna.Find(id);
            if (hizmetAna == null)
            {
                return HttpNotFound();
            }
            return View(hizmetAna);
        }

        // GET: HizmetAna/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HizmetAna/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HizmetAna hizmetAna, HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {

                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                string hizmetanaimgname = Guid.NewGuid().ToString() + imginfo.Extension; // isimlendirme.
                img.Resize(800,400); //yükseklik genişlik.
                img.Save("~/Upload/HizmetAna/" + hizmetanaimgname); // kayıt yeri.

                hizmetAna.ResimURL = "/Upload/HizmetAna/" + hizmetanaimgname;

            }

            db.HizmetAna.Add(hizmetAna);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: HizmetAna/Edit/5
            public ActionResult Edit(int? id)
             {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HizmetAna hizmetAna = db.HizmetAna.Find(id);
            if (hizmetAna == null)
            {
                return HttpNotFound();
            }
            return View(hizmetAna);
        }

        // POST: HizmetAna/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, HizmetAna hizmetAna, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.HizmetAna.Where(i => i.HizmetAnaId == id).SingleOrDefault();
                if (ResimURL != null)
                {

                    if (System.IO.File.Exists(Server.MapPath(b.ResimURL))) //Dosya var mı ? Yok mu ?
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL)); // Resim var ise Sil.
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension; // isimlendirme.
                    img.Resize(1000, 600); //yükseklik genişlik.
                    img.Save("~/Upload/HizmetAna/" + blogimgname); // kayıt yeri.

                    b.ResimURL = "/Upload/HizmetAna/" + blogimgname;
                }

                b.Aciklama = hizmetAna.Aciklama;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hizmetAna);


        }

        // GET: Hizmet/Delete/5
        public ActionResult Delete(int? id)
        {
            var b = db.HizmetAna.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ResimURL))) //Dosya var mı ? Yok mu ?
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL)); // Resim var ise Sil.
            }

            db.HizmetAna.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Hizmet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HizmetAna hizmet = db.HizmetAna.Find(id);
            db.HizmetAna.Remove(hizmet);
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
