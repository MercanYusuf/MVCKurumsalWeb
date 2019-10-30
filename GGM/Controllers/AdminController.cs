using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GGM.Models.DataContext;
using GGM.Models.Model;

namespace GGM.Controllers
{
    public class AdminController : Controller
    {
        dataContext db = new dataContext();
        [Route("Ggmadmin")]
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.BlogSay = db.Blog.Count();
            ViewBag.KategoriSay = db.Kategori.Count();
            ViewBag.HizmetSay = db.HizmetAna.Count();
            ViewBag.YorumSay = db.Yorum.Count();
            ViewBag.YorumOnay = db.Yorum.Where(i => i.Onay == false).Count();
            var sorgu = db.Kategori.ToList();
            return View(sorgu);

        }
        [Route("Ggmadmin/giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(Adminler admin/*,string sifre*/)
        {
            //var md5pass = Crypto.Hash(sifre, "MD5");
            var login = db.Adminler.Where(i => i.Email == admin.Email).SingleOrDefault();
            if (login.Email == admin.Email && login.Sifre == Crypto.Hash(admin.Sifre, "MD5")) /*md5pass*/
            {
                Session["adminid"] = login.AdminId;
                Session["email"] = login.Email;
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Uyari = "Kullanıcı eposta yada şifre yanlış.";
            return View();
        }
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["email"] = null;
            Session.Abandon(); //düşürme sessionları
            return RedirectToAction("Login", "Admin");

        }

        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string email)
        {
            var mail = db.Adminler.Where(i => i.Email == email).SingleOrDefault();
            if (mail != null )
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();

                Adminler admin=new Adminler();
                mail.Sifre = Crypto.Hash(Convert.ToString(yenisifre), "MD5");
                db.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "mail adres";
                WebMail.Password = "şifre";
                WebMail.SmtpPort = 587;
                WebMail.Send(email, "Admin Panel Giriş Şifreniz:", "Şifreniz:"+ yenisifre);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";

            }
            else
            {
                ViewBag.Uyari = "Tekrar Deneyiniz.";
            }

            return View();
        }
        public ActionResult Adminler()
        {
            return View(db.Adminler.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Adminler admin, string sifre, string eposta)
        {
            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(sifre, "MD5");
                db.Adminler.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }

            return View(admin);
        }
        public ActionResult Edit(int id)
        {
            var a = db.Adminler.Where(i => i.AdminId == id).SingleOrDefault();
            return View(a);
        }
        [HttpPost]
        public ActionResult Edit(int id, Adminler admin, string sifre, string eposta)
        {

            if (ModelState.IsValid)
            {
                var a = db.Adminler.Where(i => i.AdminId == id).SingleOrDefault();
                a.Sifre = Crypto.Hash(sifre, "MD5");
                a.Email = admin.Email;
                a.Yetki = admin.Yetki;
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }

        public ActionResult Delete(int id)
        {
            var a = db.Adminler.Where(i => i.AdminId == id).SingleOrDefault();
            if (a != null)
            {
                db.Adminler.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View();
        }

    }
}