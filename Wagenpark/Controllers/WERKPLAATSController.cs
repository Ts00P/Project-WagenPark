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
    public class WERKPLAATSController : Controller
    {
        private WagenparkDBEntities db = new WagenparkDBEntities();

        // GET: WERKPLAATS
        public ActionResult Index()
        {
            return View(db.WERKPLAATS.ToList());
        }

        // GET: WERKPLAATS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WERKPLAATS wERKPLAATS = db.WERKPLAATS.Find(id);
            if (wERKPLAATS == null)
            {
                return HttpNotFound();
            }
            return View(wERKPLAATS);
        }

        // GET: WERKPLAATS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WERKPLAATS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WerkplaatsNr,Naam")] WERKPLAATS wERKPLAATS)
        {
            if (ModelState.IsValid)
            {
                db.WERKPLAATS.Add(wERKPLAATS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wERKPLAATS);
        }

        // GET: WERKPLAATS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WERKPLAATS wERKPLAATS = db.WERKPLAATS.Find(id);
            if (wERKPLAATS == null)
            {
                return HttpNotFound();
            }
            return View(wERKPLAATS);
        }

        // POST: WERKPLAATS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WerkplaatsNr,Naam")] WERKPLAATS wERKPLAATS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wERKPLAATS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wERKPLAATS);
        }

        // GET: WERKPLAATS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WERKPLAATS wERKPLAATS = db.WERKPLAATS.Find(id);
            if (wERKPLAATS == null)
            {
                return HttpNotFound();
            }
            return View(wERKPLAATS);
        }

        // POST: WERKPLAATS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WERKPLAATS wERKPLAATS = db.WERKPLAATS.Find(id);
            db.WERKPLAATS.Remove(wERKPLAATS);
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
