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
    public class OdaTürüController : Controller
    {
        // GET: OdaTürü
        otelyönetimiEntities db = new otelyönetimiEntities();
        // GET: OdaTürü

        public ActionResult Index()
        {
            return View(db.OdaTürü.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OdaTürüid,OdaTürü1")] OdaTürü odaTürü)
        {
            if (ModelState.IsValid)
            {
                db.OdaTürü.Add(odaTürü);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(odaTürü);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OdaTürü odaTürü = db.OdaTürü.Find(id);
            if (odaTürü == null)
            {
                return HttpNotFound();
            }
            return View(odaTürü);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OdaTürüid,OdaTürü1")] OdaTürü odaTürü)
        {
            if (ModelState.IsValid)
            {
                db.Entry(odaTürü).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(odaTürü);
        }


        public ActionResult Delete(int id)
        {
            using (otelyönetimiEntities db = new otelyönetimiEntities())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                OdaTürü odaTürü = db.OdaTürü.Find(id);
                if (odaTürü == null)
                {
                    return HttpNotFound();
                }

                db.OdaTürü.Remove(odaTürü);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}