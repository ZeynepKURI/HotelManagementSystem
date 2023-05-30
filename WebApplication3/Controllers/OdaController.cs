using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class OdaController : Controller
    {
        // GET: Oda
        public ActionResult Index()
        {


            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                var model = db.Oda.ToList();

                return View(model);
            }

        }
        public ActionResult Create()
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                List<SelectListItem> values = (from x in db.OdaTürü.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.OdaTürü1,
                                                   Value = x.OdaTürüid.ToString()

                                               }).ToList();
                ViewBag.Values = values;

                List<SelectListItem> valuess = (from x in db.RezervasyonDurumu.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.RezervasyonDurumu1,
                                                    Value = x.RezervasyonDurumuid.ToString()

                                                }).ToList();
                ViewBag.Valuess = valuess;

                var model = new Oda();
            }
            return View();

        }
        [HttpPost]
        public ActionResult Create(Oda rez)
        {

            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {


                if (ModelState.IsValid)
                {


                    db.Oda.Add(rez);
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
                List<SelectListItem> values = (from x in db.OdaTürü.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.OdaTürü1,
                                                   Value = x.OdaTürüid.ToString()

                                               }).ToList();
                ViewBag.Values = values;

                List<SelectListItem> valuess = (from x in db.RezervasyonDurumu.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.RezervasyonDurumu1,
                                                    Value = x.RezervasyonDurumuid.ToString()

                                                }).ToList();
                ViewBag.Valuess = valuess;



                if (id == null)
                {
                    ViewBag.Uyarı = "Düzenlenecek Hizmet Bulunamadı";
                }
                var kkK = db.Oda.Find(id);
                if (kkK == null)
                {
                    return HttpNotFound();
                }
                return View(kkK);
            }
        }
        [HttpPost]
        public ActionResult Edit(int? id, Oda oda)
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
                var hH = db.Oda.Find(id);
                if (hH == null)
                {
                    return HttpNotFound();
                }
                db.Oda.Remove(hH);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
        }
    }
}