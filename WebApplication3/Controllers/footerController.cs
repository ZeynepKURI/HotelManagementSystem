using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class footerController : Controller
    {
        // GET: footer
        public ActionResult Index()
        {


            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                var model = db.footer.ToList();

                return View(model);
            }

        }
        public ActionResult Create()
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                var model = new footer();
            }
            return View();

        }
        [HttpPost]
        public ActionResult Create(footer rez)
        {

            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {


                if (ModelState.IsValid)
                {


                    db.footer.Add(rez);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(rez);
            }
        }
        public ActionResult Edit(int? id)
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {



                if (id == null)
                {
                    ViewBag.Uyarı = "Düzenlenecek Hizmet Bulunamadı";
                }
                var kkK = db.footer.Find(id);
                if (kkK == null)
                {
                    return HttpNotFound();
                }
                return View(kkK);
            }
        }
        [HttpPost]
        public ActionResult Edit(int? id, footer oda)
        {


            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {


                if (ModelState.IsValid)
                {


                    db.Entry(oda).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                if (id == null)
                {

                    return HttpNotFound();

                }
                var hH = db.footer.Find(id);
                if (hH == null)
                {
                    return HttpNotFound();
                }
                db.footer.Remove(hH);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
        }
    }
}