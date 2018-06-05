using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wagenpark.Models;

namespace Wagenpark.Controllers
{
    [Authorize(Roles = "Admin, Dealer")]
    public class ONDERHOUDController : Controller
    {
        private WagenparkDBEntities db = new WagenparkDBEntities();

        // GET: ONDERHOUDs
        public ActionResult Index()
        {
            var oNDERHOUD = db.ONDERHOUD.Include(o => o.CAR).Include(o => o.WERKPLAATS);
            return View(oNDERHOUD.ToList());
        }

        // GET: ONDERHOUDs/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ONDERHOUD oNDERHOUD = db.ONDERHOUD.Find(id);
            if (oNDERHOUD == null)
            {
                return HttpNotFound();
            }
            return View(oNDERHOUD);
        }

        // GET: ONDERHOUDs/Create
        public ActionResult Create()
        {
            ViewBag.Kenteken = new SelectList(db.CAR, "Kenteken", "Merk");
            ViewBag.WerkplaatsNr = new SelectList(db.WERKPLAATS, "WerkplaatsNr", "Naam");
            return View();
        }

        // POST: ONDERHOUDs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OnderhoudsDatum,Kosten,Kenteken,WerkplaatsNr")] ONDERHOUD oNDERHOUD)
        {
            if (ModelState.IsValid)
            {
                db.ONDERHOUD.Add(oNDERHOUD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Kenteken = new SelectList(db.CAR, "Kenteken", "Merk", oNDERHOUD.Kenteken);
            ViewBag.WerkplaatsNr = new SelectList(db.WERKPLAATS, "WerkplaatsNr", "Naam", oNDERHOUD.WerkplaatsNr);
            return View(oNDERHOUD);
        }

        // GET: ONDERHOUDs/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ONDERHOUD oNDERHOUD = db.ONDERHOUD.Find(id);
            if (oNDERHOUD == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kenteken = new SelectList(db.CAR, "Kenteken", "Merk", oNDERHOUD.Kenteken);
            ViewBag.WerkplaatsNr = new SelectList(db.WERKPLAATS, "WerkplaatsNr", "Naam", oNDERHOUD.WerkplaatsNr);
            return View(oNDERHOUD);
        }

        // POST: ONDERHOUDs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OnderhoudsDatum,Kosten,Kenteken,WerkplaatsNr")] ONDERHOUD oNDERHOUD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oNDERHOUD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Kenteken = new SelectList(db.CAR, "Kenteken", "Merk", oNDERHOUD.Kenteken);
            ViewBag.WerkplaatsNr = new SelectList(db.WERKPLAATS, "WerkplaatsNr", "Naam", oNDERHOUD.WerkplaatsNr);
            return View(oNDERHOUD);
        }

        // GET: ONDERHOUDs/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ONDERHOUD oNDERHOUD = db.ONDERHOUD.Find(id);
            if (oNDERHOUD == null)
            {
                return HttpNotFound();
            }
            return View(oNDERHOUD);
        }

        // POST: ONDERHOUDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            ONDERHOUD oNDERHOUD = db.ONDERHOUD.Find(id);
            db.ONDERHOUD.Remove(oNDERHOUD);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
