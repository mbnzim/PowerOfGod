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
    public class JobTitlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobTitles
        public ActionResult Index()
        {
            var jobTitles = db.jobTitles.Include(j => j.departments).Include(j => j.salaries);
            return View(jobTitles.ToList());
        }

        // GET: JobTitles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTitle jobTitle = db.jobTitles.Find(id);
            if (jobTitle == null)
            {
                return HttpNotFound();
            }
            return View(jobTitle);
        }

        // GET: JobTitles/Create
        public ActionResult Create()
        {
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName");
            ViewBag.SalaryId = new SelectList(db.salary, "SalaryId", "email");
            return View();
        }

        // POST: JobTitles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobTitleId,rate,SalaryId,deptCode")] JobTitle jobTitle)
        {
            if (ModelState.IsValid)
            {
                //var dept = db.salary.Find(jobTitle.SalaryId);
                //if(dept.employees.departments.deptName == "Cleaner")
                //{
                //    dept.Rate = 37.0;
                //}else if(dept.employees.departments.deptName == "Security")
                //{
                //    dept.Rate = 42.0;
                //}

                db.jobTitles.Add(jobTitle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName", jobTitle.deptCode);
            ViewBag.SalaryId = new SelectList(db.salary, "SalaryId", "email", jobTitle.SalaryId);
            return View(jobTitle);
        }

        // GET: JobTitles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTitle jobTitle = db.jobTitles.Find(id);
            if (jobTitle == null)
            {
                return HttpNotFound();
            }
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName", jobTitle.deptCode);
            ViewBag.SalaryId = new SelectList(db.salary, "SalaryId", "email", jobTitle.SalaryId);
            return View(jobTitle);
        }

        // POST: JobTitles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobTitleId,rate,SalaryId,deptCode")] JobTitle jobTitle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobTitle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName", jobTitle.deptCode);
            ViewBag.SalaryId = new SelectList(db.salary, "SalaryId", "email", jobTitle.SalaryId);
            return View(jobTitle);
        }

        // GET: JobTitles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTitle jobTitle = db.jobTitles.Find(id);
            if (jobTitle == null)
            {
                return HttpNotFound();
            }
            return View(jobTitle);
        }

        // POST: JobTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobTitle jobTitle = db.jobTitles.Find(id);
            db.jobTitles.Remove(jobTitle);
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
