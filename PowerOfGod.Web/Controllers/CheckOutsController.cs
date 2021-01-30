using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Salary;

namespace PowerOfGod.Web.Controllers
{
    public class CheckOutsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CheckOuts
        public ActionResult Index()
        {
            var checkOuts = db.checkOuts.Include(c => c.salaries);
            return View(checkOuts.ToList());
        }

        // GET: CheckOuts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.checkOuts.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // GET: CheckOuts/Create
        public ActionResult Create()
        {
            ViewBag.SalaryId = new SelectList(db.salary, "SalaryId", "email");
            return View();
        }

        // POST: CheckOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckOutId,endTime,Date,email,SalaryId")] CheckOut checkOut)
        {
            // UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var checkuser = db.checkIns.Where(x => x.Date == checkOut.Date && x.email == checkOut.User.Email);
            // var sal = db.salary.Find(checkOut.SalaryId);
            // checkOut.User = usermanager.FindByEmail(User.Identity.Name);
            //if (sal != null)
            //{
            //    var salary = new Salary()
            //    {
            //        //SalaryId =  checkuser.
            //        //Date = checkOut.Date,
            //        endTime = checkOut.endTime,
            //        //email = checkOut.email
            //    };
            //    db.salary.Add(salary);
            //    db.SaveChanges();
            //}
            checkOut.Date = DateTime.Now.Date;
            checkOut.endTime = DateTime.Now.TimeOfDay;
            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (ModelState.IsValid)
            {         
                //var checkuser = db.checkIns.Where(x => x.Date == checkOut.Date && x.email == checkOut.User.Email);
                var sal = db.salary.Find(checkOut.SalaryId);
                checkOut.User = usermanager.FindByEmail(User.Identity.Name);
                //checkIn.User = usermanager.FindByEmail(User.Identity.Name);
                checkOut.email = checkOut.User.Email;
                sal.endTime = checkOut.endTime;
                sal.Rate = 42;
                sal.hoursWorked = sal.CalcHourWorked();
                sal.wage = sal.CalcHourWorked() * sal.Rate;

                db.checkOuts.Add(checkOut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalaryId = new SelectList(db.salary, "SalaryId", "email", checkOut.SalaryId);
            return View(checkOut);
        }

        // GET: CheckOuts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.checkOuts.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalaryId = new SelectList(db.salary, "SalaryId", "email", checkOut.SalaryId);
            return View(checkOut);
        }

        // POST: CheckOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckOutId,endTime,Date,email,SalaryId")] CheckOut checkOut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkOut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SalaryId = new SelectList(db.salary, "SalaryId", "email", checkOut.SalaryId);
            return View(checkOut);
        }

        // GET: CheckOuts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.checkOuts.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // POST: CheckOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckOut checkOut = db.checkOuts.Find(id);
            db.checkOuts.Remove(checkOut);
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
