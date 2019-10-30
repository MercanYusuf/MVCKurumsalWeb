using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GGM.Models.DataContext;
using GGM.Models.Model;

namespace GGM.Controllers
{
    public class SiteKimlikController : Controller
    {
        private dataContext db = new dataContext();
        // GET: SiteKimlik
        public ActionResult Index()
        {
            return View(db.SiteKimlik.ToList());
        }
        //Get/SiteKimlik/
        public ActionResult Edit(int id)
        {
            var kimlik = db.SiteKimlik.Where(i => i.KimlikId == id).SingleOrDefault();
            return View(kimlik);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        //Post/SiteKimlik

        public ActionResult Edit(int id, SiteKimlik kimlik, HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid)
            {
                var K = db.SiteKimlik.Where(i => i.KimlikId == id).SingleOrDefault();

                if (LogoURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(K.LogoURL))) //Dosya var mı ? Yok mu ?
                    {
                        System.IO.File.Delete(Server.MapPath(K.LogoURL)); // Resim var ise Sil.
                    }

                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imginfo = new FileInfo(LogoURL.FileName);

                    string logoname = LogoURL.FileName + imginfo.Extension; // isimlendirme.
                    img.Resize(300, 200); //yükseklik genişlik.
                    img.Save("~/Upload/Kimlik/" + logoname); // kayıt yeri.

                    K.LogoURL = "/Upload/Kimlik/" + logoname;
                }

                K.Title = kimlik.Title;
                K.Keywords = kimlik.Keywords;
                K.Description = kimlik.Description;
                K.Unvan = kimlik.Unvan;


                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(kimlik);
        }
    }
}