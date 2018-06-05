using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wagenpark.Models;
using System.Dynamic;

namespace Wagenpark.Controllers
{
    public class AppController : Controller
    {
        private WagenparkDBEntities db = new WagenparkDBEntities();

        // GET: App
        public ActionResult Index()
        {
            return View(db);
        }
    }
}