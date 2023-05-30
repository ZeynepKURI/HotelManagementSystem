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
    public class hizmetController : Controller
    {
        // GET: hizmet
        otelyönetimiEntities db = new otelyönetimiEntities();
        // GET: hizmet
        public ActionResult Index()
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                var model = db.Service1.ToList();

                return View(model);
            }

        }
        public ActionResult Create()
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                ViewBag.id = new SelectList(db.Service1, "id", "başlık1", "açıklama1");
                return View();
            }


        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Service1 service, HttpPostedFileBase resim1)
        {

            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {


                if (ModelState.IsValid)
                {


                    if (resim1 != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(service.resim1)))
                        {
                            System.IO.File.Delete(Server.MapPath(service.resim1));
                        }

                        WebImage img = new WebImage(resim1.InputStream);
                        FileInfo imgİnfo = new FileInfo(resim1.FileName);

                        string resimname = Guid.NewGuid().ToString() + imgİnfo.Extension;
                        img.Resize(250, 100);
                        img.Save("~/Uploads/ımage/" + resimname);
                        service.resim1 = "/Uploads/ımage/" + resimname;
                    }

                    db.Service1.Add(service);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(service);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var about = db.Service1.Where(a => a.id == id).SingleOrDefault();
            return View(about);
        }

        // POST: About/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Service1 about, HttpPostedFileBase resim1)
        {
            if (ModelState.IsValid)
            {
                var ab = db.Service1.Where(a => a.id == id).SingleOrDefault();

                if (resim1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(about.resim1)))
                    {
                        System.IO.File.Delete(Server.MapPath(about.resim1));
                    }

                    WebImage img = new WebImage(resim1.InputStream);
                    FileInfo imgİnfo = new FileInfo(resim1.FileName);

                    string resimname = Guid.NewGuid().ToString() + imgİnfo.Extension;
                    img.Resize(250, 100);
                    img.Save("~/Uploads/ımage/" + resimname);
                    ab.resim1 = "/Uploads/ımage/" + resimname;
                }
                ab.başlık1 = about.başlık1;
                ab.açıklama1 = about.açıklama1;

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
                var hH = db.Service1.Find(id);
                if (hH == null)
                {
                    return HttpNotFound();
                }
                db.Service1.Remove(hH);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
        }
    }
}