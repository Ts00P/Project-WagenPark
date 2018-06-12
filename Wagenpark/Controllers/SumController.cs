using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wagenpark.Models;
using System.Dynamic;

namespace Wagenpark.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SumController : Controller
    {
        private WagenparkDBEntities db = new WagenparkDBEntities();

        // GET: Sum
        public ActionResult Index()
        {
            return View(db);
        }
    }
}