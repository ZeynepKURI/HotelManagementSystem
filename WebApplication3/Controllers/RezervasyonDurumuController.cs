using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class RezervasyonDurumuController : Controller
    {
        // GET: RezervasyonDurumu
        otelyönetimiEntities db = new otelyönetimiEntities();
        // GET: RezervasyonDurumu
        public ActionResult Index()
        {
            return View(db.RezervasyonDurumu.ToList());

        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RezervasyonDurumuid,RezervasyonDurumu1")] RezervasyonDurumu rezervasyonDurumu)
        {
            if (ModelState.IsValid)
            {
                db.RezervasyonDurumu.Add(rezervasyonDurumu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rezervasyonDurumu);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RezervasyonDurumu rezervasyonDurumu = db.RezervasyonDurumu.Find(id);
            if (rezervasyonDurumu == null)
            {
                return HttpNotFound();
            }
            return View(rezervasyonDurumu);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RezervasyonDurumuid,RezervasyonDurumu1")] RezervasyonDurumu rezervasyonDurumu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervasyonDurumu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rezervasyonDurumu);
        }
        public ActionResult Delete(int id)
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                RezervasyonDurumu odaTürü = db.RezervasyonDurumu.Find(id);
                if (odaTürü == null)
                {
                    return HttpNotFound();
                }

                db.RezervasyonDurumu.Remove(odaTürü);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}