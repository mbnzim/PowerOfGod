using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Salary;

namespace PowerOfGod.Web.Controllers
{
    public class SalariesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Salaries
        public ActionResult Index()
        {
            var salary = db.salary.Include(s => s.employees);
            return View(salary.ToList());
        }

        // GET: Salaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.salary.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // GET: Salaries/Create
        public ActionResult Create()
        {
            ViewBag.EmpNum = new SelectList(db.employees, "EmpNum", "firstName");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaryId,startTime,endTime,Date,hoursWorked,Rate,email,wage,EmpNum")] Salary salary)
        {
            var name = db.employees.Find(salary.EmpNum);
            if (ModelState.IsValid)
            {
              
                db.salary.Add(salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpNum = new SelectList(db.employees, "EmpNum", "firstName", salary.EmpNum);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.salary.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpNum = new SelectList(db.employees, "EmpNum", "firstName", salary.EmpNum);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryId,startTime,endTime,Date,hoursWorked,Rate,email,wage,EmpNum")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpNum = new SelectList(db.employees, "EmpNum", "firstName", salary.EmpNum);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.salary.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary salary = db.salary.Find(id);
            db.salary.Remove(salary);
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

        public ActionResult Search(string search)
        {
            var pay = from x in db.salary select x;
            try
            {
                if (!String.IsNullOrEmpty(search))
                {

                    pay = pay.Where(z => z.email == search);
                    ViewBag.pay = pay.Where(z => z.email == search).Sum(x => x.wage);
                }
                else
                {
                    ViewBag.Message = "Please enter a email address to search";
                }

            }
            catch (Exception e)
            {
                ViewBag.Message = "No match found";
            }


            return View(pay);
        }
    }
}
