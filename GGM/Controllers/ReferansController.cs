using System;
using System.Collections.Generic;
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
    public class ReferansController : Controller
    {
        dataContext db = new dataContext();
        // GET: Referans
        public ActionResult Index()
        {
            return View(db.Referanslar.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Referanslar referanslar, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string referansimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(200, 250);
                    img.Save("~/Upload/Referans/" + referansimgname);

                    referanslar.ResimURL = "/Upload/Referans/" + referansimgname;
                }

                db.Referanslar.Add(referanslar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(referanslar);
        }
        
        // GET: Hizmet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek Hizmet Bulunamadı";
            }
            var referans = db.Referanslar.Find(id);
            if (referans == null)
            {
                return HttpNotFound();
            }

            return View(referans);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Edit(int? id, Referanslar referanslar, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var h = db.Referanslar.Where(x => x.ReferansId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string referansimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(200, 250);
                    img.Save("~/Upload/Referans/" + referansimgname);

                    h.ResimURL = "/Upload/Referans/" + referansimgname;
                }

                h.ReferansAd = referanslar.ReferansAd;
               
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Referanslar referanslar = db.Referanslar.Find(id);
            db.Referanslar.Remove(referanslar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}