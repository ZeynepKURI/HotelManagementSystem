using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class AdminController : Controller
    { 
        otelyönetimiEntities db = new otelyönetimiEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admin.Where(x => x.KullanıcıAdı == admin.KullanıcıAdı).SingleOrDefault();
            if (login.KullanıcıAdı == admin.KullanıcıAdı && login.Şifre == admin.Şifre)
            {
                Session["adminid"] = login.Adminid;
                Session["kullanıcıadı"] = login.KullanıcıAdı;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Uyarı = "Kullanıcı adı yada şifre yanlış";
            return View(admin);
        }
        public ActionResult Logout()
        {

            Session["adminid"] = null;
            Session["kullanıcıadı"] = null;

            Session.Abandon();
            return RedirectToAction("Login", "Admin");

        }
    }
}
