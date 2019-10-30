using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GGM.Models.DataContext;
using GGM.Models.Model;
using PagedList;

namespace GGM.Controllers
{
    public class HomeController : Controller
    {
        private dataContext db = new dataContext();

        [Route("")]
        [Route("Anasayfa")]
        public ActionResult Index()
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();
            return View();
        }

        //Partial Başlangıç
        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(i => i.SliderId));
        }

        public ActionResult MusteriYorumlariPartial()
        {
            return View(db.MusteriYorumlari.ToList().OrderByDescending(i => i.MusteriYorumlariId));
        }

        public ActionResult HizmetAnaPartial()
        {
            return View(db.HizmetAna.ToList().OrderByDescending(i => i.HizmetAnaId));
        }

        public ActionResult BlogKategoriPartial()
        {
            return PartialView(db.Kategori.Include("Blogs").ToList().OrderByDescending(i => i.KategoriAd));
        }

        public ActionResult BlogKayitPartial()
        {
            return PartialView(db.Blog.ToList().OrderByDescending(i => i.BlogId));
        }

        //Partial Bitiş
        [Route("BlogPost")]
        public ActionResult Blog(int Page = 1)
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();
            return View(db.Blog.Include("Kategori").OrderByDescending(i => i.BlogId).ToPagedList(Page, 5));
        }

        [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();
            var b = db.Blog.Include("Kategori").Include("Yorums").Where(i => i.BlogId == id).SingleOrDefault();
            return View(b);
        }

        [Route("BlogPost/{kategoriad}/{id:int}")]
        public ActionResult KategoriBlog(int id, int Sayfa = 1)
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();

            var b = db.Blog.Include("Kategori").OrderByDescending(x => x.BlogId).Where(x => x.Kategori.KategoriId == id)
                .ToPagedList(Sayfa, 5);
            return View(b);
        }

        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int blogid)
        {
            if (icerik == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            db.Yorum.Add(new Yorum
            {
                AdSoyad = adsoyad,
                Eposta = eposta,
                Icerik = icerik,
                BlogId = blogid,
                Onay = false
            });
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [Route("Hakkimiz")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();
            return View(db.Hakkimizda.SingleOrDefault());
        }

        [Route("Referanslarimiz")]
        public ActionResult Referans()
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();
            return View(db.Referanslar.ToList().OrderByDescending(i => i.ReferansId));
        }

        [Route("Hizmetlerimiz")]
        public ActionResult Hizmet()
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();
            return View(db.Hizmet.SingleOrDefault());
        }

        [Route("BizeUlasin")]
        public ActionResult Iletisim()
        {

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }

        [HttpPost]

        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null,
            string mesaj = null)
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();
            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "mail.yusufmercan.me";
                WebMail.EnableSsl = false;
                WebMail.UserName = "info1@yusufmercan.me";
                WebMail.Password = "yusuf1903";
                WebMail.SmtpPort = 587;
                WebMail.Send("info@yusufmercan.me", konu, email, mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";
                Response.Redirect("/Iletisim");

            }
            else
            {
                ViewBag.Uyari = "Tekrar Deneyiniz.";
            }

            return View();
        }




        //[HttpPost]
        //public ActionResult Iletisim(Sendmail model)
        //{
        //    string server = ConfigurationManager.AppSettings["server"];
        //    int port = int.Parse(ConfigurationManager.AppSettings["port"]);
        //    bool ssl = ConfigurationManager.AppSettings["ssl"].ToString() == "1" ? true : false;
        //    string from = ConfigurationManager.AppSettings["from"];
        //    string password = ConfigurationManager.AppSettings["password"];
        //    string fromname = ConfigurationManager.AppSettings["fromname"];
        //    string to = ConfigurationManager.AppSettings["to"];

        //    var client = new SmtpClient();
        //    client.Host = server;
        //    client.Port = port;
        //    client.EnableSsl = ssl;
        //    client.UseDefaultCredentials = true;
        //    client.Credentials = new System.Net.NetworkCredential(from, password);


        //    var email = new MailMessage();
        //    email.From = new MailAddress(from, fromname);
        //    email.To.Add(to);

        //    email.Subject = model.konu;
        //    email.IsBodyHtml = true;
        //    email.Body =
        //        $"ad soyad : {model.adsoyad} \n konu: {model.konu} \n mesaj : {model.mesaj} \n eposta : {model.mesaj} \n eposta : {model.email}";

        //    try
        //    {
        //        client.Send(email);
        //        ViewData["result"] = true;
        //    }
        //    catch (Exception e)
        //    {

        //        ViewData["result"] = false;
        //    }


        //    return RedirectToAction("Index");
        //}

        public ActionResult FooterPartial()
        {

            ViewBag.SiteKimlik = db.SiteKimlik.SingleOrDefault();
            //ViewBag.Blog = db.Blog.ToList().OrderByDescending(i => i.BlogId);

            ViewBag.HizmetAna = db.HizmetAna.Take(8).ToList().OrderByDescending(i => i.HizmetAnaId);

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault(); //viewbag ile ana sayfaya tasıyoruz.
            return PartialView();
        }

    }
}
