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
    public class HakkimizdaController : Controller
    {
        private dataContext db = new dataContext();
        

        // GET: Hakkimizda
        public ActionResult Index()
        {
            return View(db.Hakkimizda.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: HizmetAna/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Create(Hakkimizda hakkimizda, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
               
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string hakkimizdaimg = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Upload/Hakkimizda/" + hakkimizdaimg);
                    hakkimizda.ResimURL = "/Upload/Hakimizda/" + hakkimizdaimg;

                }
                db.Hakkimizda.Add(hakkimizda);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(hakkimizda);
        }

   
    // GET: Hakkimizda/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hakkimizda hakkimizda = db.Hakkimizda.Find(id);
            if (hakkimizda == null)
            {
                return HttpNotFound();
            }
            return View(hakkimizda);
        }
        // POST: Hakkimizda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Hakkimizda hakkimizda, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var K = db.Hakkimizda.Where(i => i.HakkimizdaId == id).SingleOrDefault();

                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(K.ResimURL))) //Dosya var mı ? Yok mu ?
                    {
                        System.IO.File.Delete(Server.MapPath(K.ResimURL)); // Resim var ise Sil.
                    }

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string hakkimizdaimgname = ResimURL.FileName + imginfo.Extension; // isimlendirme.
                    img.Resize(500, 500); //yükseklik genişlik.
                    img.Save("~/Upload/Hakkimizda/" + hakkimizdaimgname); // kayıt yeri.

                    K.ResimURL = "/Upload/Hakkimizda/" + hakkimizdaimgname;
                }

                K.Aciklama = hakkimizda.Aciklama;
                K.Misyon = hakkimizda.Misyon;
                K.Vizyon = hakkimizda.Vizyon;
                K.KalitePolitikamiz = hakkimizda.KalitePolitikamiz;
                K.Kurumsal = hakkimizda.Kurumsal;
                K.Degerlerimiz = hakkimizda.Degerlerimiz;
                

                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(hakkimizda);
        }
    }
}