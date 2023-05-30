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
    public class roomController : Controller
    {
        // GET: room
        otelyönetimiEntities db = new otelyönetimiEntities();
        public ActionResult Index()
        {


            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                var model = db.room.ToList();

                return View(model);
            }

        }
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.room, "id", "Başlık", "Açıklama");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(room rez, HttpPostedFileBase Resim)
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
                        img.Save("~/Uploads/room/" + resimname);
                        rez.Resim = "/Uploads/room/" + resimname;
                    }


                    db.room.Add(rez);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(rez);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var about = db.room.Where(a => a.id == id).SingleOrDefault();
            return View(about);
        }

        // POST: footer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, room about, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                var ab = db.room.Where(a => a.id == id).SingleOrDefault();

                if (Resim != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(about.Resim)))
                    {
                        System.IO.File.Delete(Server.MapPath(about.Resim));
                    }

                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo imgİnfo = new FileInfo(Resim.FileName);

                    string resimname = Guid.NewGuid().ToString() + imgİnfo.Extension;
                    img.Resize(250, 100);
                    img.Save("~/Uploads/room/" + resimname);
                    ab.Resim = "/Uploads/room/" + resimname;
                }
                ab.Başlık = about.Başlık;
                ab.Açıklama = about.Açıklama;
                ab.Fiyat = about.Fiyat;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);
        }

        public ActionResult Delete(int id)
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                if (id == null)
                {

                    return HttpNotFound();

                }
                var hH = db.room.Find(id);
                if (hH == null)
                {
                    return HttpNotFound();
                }
                db.room.Remove(hH);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
        }
    }
}