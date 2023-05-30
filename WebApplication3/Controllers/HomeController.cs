using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private otelyönetimiEntities db = new otelyönetimiEntities();
        public ActionResult Index()
        {
            ViewBag.Service1 = db.Service1.ToList().OrderByDescending(x => x.id);
            ViewBag.room = db.room.OrderByDescending(x => x.id).Take(3).ToList();
            ViewBag.Anasayfa = db.Home.ToList().OrderByDescending(x => x.id);
            ViewBag.About = db.About.ToList().OrderByDescending(x => x.id);
            ViewBag.footer = db.footer.ToList().OrderByDescending(x => x.id);
            ViewBag.Galeri = db.galeri.ToList().OrderByDescending(x => x.id);

            return View();


        }

        public ActionResult About()
        {
            ViewBag.Service1 = db.Service1.ToList().OrderByDescending(x => x.id);
            ViewBag.room = db.room.OrderByDescending(x => x.id).Take(3).ToList();
            ViewBag.Anasayfa = db.Home.ToList().OrderByDescending(x => x.id);
            ViewBag.About = db.About.ToList().OrderByDescending(x => x.id);
            ViewBag.footer = db.footer.ToList().OrderByDescending(x => x.id);
            ViewBag.Galeri = db.galeri.ToList().OrderByDescending(x => x.id);

            return View();
        }
        public ActionResult Rezervasyon()
        {
            return View();
        }

        public ActionResult Service()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }

        public ActionResult Rooms()
        {

           

            ViewBag.Service1 = db.Service1.ToList().OrderByDescending(x => x.id);
            ViewBag.room = db.room.OrderByDescending(x => x.id).Take(3).ToList();
            ViewBag.Anasayfa = db.Home.ToList().OrderByDescending(x => x.id);
            ViewBag.About = db.About.ToList().OrderByDescending(x => x.id);
            ViewBag.footer = db.footer.ToList().OrderByDescending(x => x.id);
            ViewBag.Galeri = db.galeri.ToList().OrderByDescending(x => x.id);

            return View();

        }
        public ActionResult footer()
        {


            return View(db.footer.ToList());

        }

        public ActionResult AboutPartial()
        {


            return View(db.About.ToList());

        }

        public ActionResult AnasayfaPartial()
        {


            return View(db.Home.ToList());

        }
        public ActionResult roomPartial()
        {


            return PartialView(db.room.ToList().OrderBy(x => x.Başlık));

        }
        public ActionResult Service1Partial()
        {


            return View(db.Service1.ToList());

        }
        public ActionResult GaleriPartial()
        {


            return View(db.galeri.ToList());

        }
    }
}