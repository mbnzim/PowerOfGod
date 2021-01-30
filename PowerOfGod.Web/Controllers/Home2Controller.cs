using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerOfGod.Web.Controllers
{
    public class Home2Controller : Controller
    {
        public ActionResult Index()
        {
            TempData["Success"] = true;
            return View();
        }
    }
}