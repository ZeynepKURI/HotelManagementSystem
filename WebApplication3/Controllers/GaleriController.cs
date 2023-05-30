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
    public class GaleriController : Controller
    {
        // GET: Galeri
        otelyönetimiEntities db = new otelyönetimiEntities();
        public ActionResult Index()
        {


            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                var model = db.galeri.ToList();

                return View(model);
            }

        }

        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.galeri, "id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(galeri rez, HttpPostedFileBase resim)
        {

            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {


                if (ModelState.IsValid)
                {


                    if (resim != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(rez.resim)))
                        {
                            System.IO.File.Delete(Server.MapPath(rez.resim));
                        }

                        WebImage img = new WebImage(resim.InputStream);
                        FileInfo imgİnfo = new FileInfo(resim.FileName);

                        string resimname = Guid.NewGuid().ToString() + imgİnfo.Extension;
                        img.Resize(50, 100);
                        img.Save("~/Uploads/galeri/" + resimname);
                        rez.resim = "/Uploads/galeri/" + resimname;
                    }


                    db.galeri.Add(rez);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(rez);
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var about = db.galeri.Where(a => a.id == id).SingleOrDefault();
            return View(about);
        }

        // POST: About/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, galeri home, HttpPostedFileBase resim)
        {
            if (ModelState.IsValid)
            {
                var ab = db.galeri.Where(a => a.id == id).SingleOrDefault();

                if (resim != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(home.resim)))
                    {
                        System.IO.File.Delete(Server.MapPath(home.resim));
                    }

                    WebImage img = new WebImage(resim.InputStream);
                    FileInfo imgİnfo = new FileInfo(resim.FileName);

                    string resimname = Guid.NewGuid().ToString() + imgİnfo.Extension;
                    img.Resize(250, 100);
                    img.Save("~/Uploads/anasayfa/" + resimname);
                    ab.resim = "/Uploads/anasayfa/" + resimname;
                }


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
                var hH = db.galeri.Find(id);
                if (hH == null)
                {
                    return HttpNotFound();
                }
                db.galeri.Remove(hH);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
        }

    }
}