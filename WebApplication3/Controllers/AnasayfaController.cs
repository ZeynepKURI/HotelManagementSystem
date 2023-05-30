using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        otelyönetimiEntities db = new otelyönetimiEntities();
        public ActionResult Index()
        {


            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                var model = db.Home.ToList();

                return View(model);
            }

        }

        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.Home, "id", "Başlık", "Açıklama");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Home rez, HttpPostedFileBase Resim)
        {

            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {


                if (ModelState.IsValid)
                {


                    if (Resim != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(rez.Resim)))
                        {
                            System.IO.File.Delete(Server.MapPath(rez.Resim));
                        }

                        WebImage img = new WebImage(Resim.InputStream);
                        FileInfo imgİnfo = new FileInfo(Resim.FileName);

                        string resimname = Guid.NewGuid().ToString() + imgİnfo.Extension;
                        img.Resize(50, 100);
                        img.Save("~/Uploads/anasayfa/" + resimname);
                        rez.Resim = "/Uploads/anasayfa/" + resimname;
                    }


                    db.Home.Add(rez);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(rez);
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var about = db.Home.Where(a => a.id == id).SingleOrDefault();
            return View(about);
        }

        // POST: About/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Home home, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                var ab = db.Home.Where(a => a.id == id).SingleOrDefault();

                if (Resim != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(home.Resim)))
                    {
                        System.IO.File.Delete(Server.MapPath(home.Resim));
                    }

                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo imgİnfo = new FileInfo(Resim.FileName);

                    string resimname = Guid.NewGuid().ToString() + imgİnfo.Extension;
                    img.Resize(250, 100);
                    img.Save("~/Uploads/anasayfa/" + resimname);
                    ab.Resim = "/Uploads/anasayfa/" + resimname;
                }
                ab.Başlık = home.Başlık;
                ab.Açıklama = home.Açıklama;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(home);
        }

        public ActionResult Delete(int id)
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                if (id == null)
                {

                    return HttpNotFound();

                }
                var hH = db.Home.Find(id);
                if (hH == null)
                {
                    return HttpNotFound();
                }
                db.Home.Remove(hH);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
        }
    }
}