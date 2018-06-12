using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wagenpark.Models;

namespace Wagenpark.Controllers
{
    [Authorize(Roles = "Admin, Dealer")]
    public class KostenController : Controller
    {

        private WagenparkDBEntities db = new WagenparkDBEntities();

        DateTime dt = DateTime.Now.AddMonths(-6);
        DateTime dtn = DateTime.Now;

        // GET: Kosten
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var oNDERHOUD = db.ONDERHOUD.Where(o => o.OnderhoudsDatum >= dt && o.OnderhoudsDatum <= dtn).Where(o => o.Paid == 0);
                return View(oNDERHOUD.ToList());
            }
            else
            {
                var oNDERHOUD = db.ONDERHOUD.Where(o => o.CAR.DEALER.Naam == User.Identity.Name).Where(o => o.OnderhoudsDatum >= dt && o.OnderhoudsDatum <= dtn).Where(o => o.Paid == 0);
                return View(oNDERHOUD.ToList());
            }
        }
    }
}