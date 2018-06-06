using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wagenpark.Models;

namespace Wagenpark.Controllers
{
    public class KostenController : Controller
    {

        private WagenparkDBEntities db = new WagenparkDBEntities();

        // GET: Kosten
        public ActionResult Index()
        {
            DateTime dt = DateTime.Now.AddMonths(-6);
            if (this.User.IsInRole("Admin"))
            {
                var oNDERHOUD = db.ONDERHOUD;
                return View(oNDERHOUD.ToList());
            }
            else
            {
                var oNDERHOUD = db.ONDERHOUD.Where(o => o.CAR.DEALER.Naam == User.Identity.Name).Where(o => dt > o.OnderhoudsDatum );
                return View(oNDERHOUD.ToList());
            }
        }
    }
}