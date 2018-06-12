using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;
using Wagenpark.Models;

namespace Wagenpark.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DEALERController : Controller
    {
        private WagenparkDBEntities db = new WagenparkDBEntities();

        private static ApplicationDbContext context = new ApplicationDbContext();

        // GET: DEALERs
        public ActionResult Index()
        {
            return View(db.DEALER.ToList());
        }

        // GET: DEALERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEALER dEALER = db.DEALER.Find(id);
            if (dEALER == null)
            {
                return HttpNotFound();
            }
            return View(dEALER);
        }

        // GET: DEALERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DEALERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DealerNr,Naam")] DEALER dEALER)
        {
            if (ModelState.IsValid)
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var user = context.Users.FirstOrDefault(u => u.UserName.Equals(dEALER.Naam, StringComparison.CurrentCultureIgnoreCase));

                manager.AddToRole(user.Id, "Dealer");
                context.SaveChanges();

                db.DEALER.Add(dEALER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dEALER);
        }

        // GET: DEALERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEALER dEALER = db.DEALER.Find(id);
            if (dEALER == null)
            {
                return HttpNotFound();
            }
            return View(dEALER);
        }

        // POST: DEALERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DealerNr,Naam")] DEALER dEALER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dEALER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dEALER);
        }

        // GET: DEALERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEALER dEALER = db.DEALER.Find(id);
            if (dEALER == null)
            {
                return HttpNotFound();
            }
            return View(dEALER);
        }

        // POST: DEALERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DEALER dEALER = db.DEALER.Find(id);

            var user = context.Users.FirstOrDefault(u => u.UserName.Equals(dEALER.Naam, StringComparison.CurrentCultureIgnoreCase));
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            if (manager.IsInRole(user.Id, "Dealer"))
            {
                manager.RemoveFromRole(user.Id, "Dealer");
                context.SaveChanges();
            }

            db.DEALER.Remove(dEALER);
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
