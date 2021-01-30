using Microsoft.Ajax.Utilities;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Employee;
using PowerOfGod.Domain.Entity.Memberss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerOfGod.Web.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Employees> Employees()
        {
            return db.employees.ToList();
        }
        public List<Members> Members()
        {
            return db.members.ToList();
        }
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            ViewBag.getEmployee = Employees().Count;
            ViewBag.getMembers = Members().Count();
            ViewBag.getStock = 50;
            return View();
        }
    }
}