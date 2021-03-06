﻿using System;
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
    public class CARController : Controller
    {
        private WagenparkDBEntities db = new WagenparkDBEntities();

        // GET: CAR
        public ActionResult Index()
        {
            if(this.User.IsInRole("Admin"))
            {
                var cAR = db.CAR.Include(c => c.DEALER);
                return View(cAR.ToList());
            }
            else
            {
                var cAR = db.CAR.Include(c => c.DEALER).Where(c => c.DEALER.Naam == User.Identity.Name);
                return View(cAR.ToList());
            }
        }

        // GET: CAR/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR cAR = db.CAR.Find(id);
            if (cAR == null)
            {
                return HttpNotFound();
            }
            return View(cAR);
        }

        // GET: CAR/Create
        public ActionResult Create()
        {
            if(User.IsInRole("Admin"))
            {
                ViewBag.DealerNr = new SelectList(db.DEALER, "DealerNr", "Naam");
            }
            else 
            {
                ViewBag.DealerNr = new SelectList(db.DEALER.Where(d => d.Naam == User.Identity.Name), "DealerNr", "Naam");
            }
            return View();
        }

        // POST: CAR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Kenteken,Merk,AutoType,DealerNr")] CAR cAR)
        {
            if (ModelState.IsValid)
            {
                cAR.InUse = 1;
                db.CAR.Add(cAR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DealerNr = new SelectList(db.DEALER, "DealerNr", "Naam");
            return View(cAR);
        }

        // GET: CAR/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR cAR = db.CAR.Find(id);
            if (cAR == null)
            {
                return HttpNotFound();
            }
            ViewBag.DealerNr = new SelectList(db.DEALER, "DealerNr", "Naam", cAR.DealerNr);
            return View(cAR);
        }

        // POST: CAR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Kenteken,Merk,AutoType,DealerNr")] CAR cAR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DealerNr = new SelectList(db.DEALER, "DealerNr", "Naam", cAR.DealerNr);
            return View(cAR);
        }

        // GET: CAR/Delete/5
        public ActionResult Change(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR cAR = db.CAR.Find(id);
            if (cAR == null)
            {
                return HttpNotFound();
            }
            return View(cAR);
        }

        // POST: CAR/Delete/5
        [HttpPost, ActionName("Change")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeConfirmed(string id)
        {
            CAR cAR = db.CAR.Find(id);
            if(cAR.InUse == 1)
                cAR.InUse = 0;
            else
                cAR.InUse = 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CAR/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR cAR = db.CAR.Find(id);
            if (cAR == null)
            {
                return HttpNotFound();
            }
            return View(cAR);
        }

        // POST: CAR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            CAR cAR = db.CAR.Find(id);
            db.CAR.Remove(cAR);
            try
            {
                db.SaveChanges();
            }
            catch (Exception err)
            {
                ModelState.AddModelError("error", err.Message);
            }
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
