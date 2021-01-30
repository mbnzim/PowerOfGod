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
    public class CheckInsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CheckIns
        public ActionResult Index()
        {
            return View(db.checkIns.ToList());
        }

        // GET: CheckIns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = db.checkIns.Find(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // GET: CheckIns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckIns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckInId,startTime,Date,email")] CheckIn checkIn)
        {
            checkIn.Date = DateTime.Now.Date;
            checkIn.startTime = DateTime.Now.TimeOfDay;
            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            checkIn.User = usermanager.FindByEmail(User.Identity.Name);
            var salary = new Salary()
            {

                Date = checkIn.Date,
                startTime = checkIn.startTime,
                //endTime = new TimeSpan(0, 0, 0, 0, 0),
                email = checkIn.User.Email,
                firstName=checkIn.User.fullName
                //hoursWorked = 0,
                //Rate = 0.0

            };
            db.salary.Add(salary);
            db.SaveChanges();

            if (ModelState.IsValid)
            {
                checkIn.User = usermanager.FindByEmail(User.Identity.Name);
                checkIn.email = checkIn.User.Email;               
           

                db.checkIns.Add(checkIn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(checkIn);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = db.checkIns.Find(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // POST: CheckIns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckInId,startTime,Date,email")] CheckIn checkIn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkIn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checkIn);
        }

        // GET: CheckIns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = db.checkIns.Find(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // POST: CheckIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckIn checkIn = db.checkIns.Find(id);
            db.checkIns.Remove(checkIn);
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
