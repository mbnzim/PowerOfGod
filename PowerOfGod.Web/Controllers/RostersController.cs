using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Employee;
using PowerOfGod.ViewModel.EmployeeViewModel;
using PowerOfGod.Business.Logic;

namespace PowerOfGod.Web.Controllers
{
    public class RostersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rosters
        public ActionResult Index(int? page)
        {
            var roster = db.rosters.Include(r => r.departments).Include(r => r.employees);
            // return View(roster.ToList());
            var query = roster.OrderBy(m => m.Date).ToList(); ;

            int PageSize = 6;
            int PageNumber = (page ?? 1);
            return View(query.ToPagedList(PageNumber, PageSize));
        }
        //changing date of employee
        //public ActionResult SpawDate()
        // {
        //     Logic c = new Logic();
        //     return View();
        // }


        // GET: Rosters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roster roster = db.rosters.Find(id);
            if (roster == null)
            {
                return HttpNotFound();
            }
            return View(roster);
        }

        // GET: Rosters/Create
        public ActionResult Create()
        {
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName");
            ViewBag.EmpNum = new SelectList(db.employees, "EmpNum", "firstName");
            return View();
        }

        // POST: Rosters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "code,Date,startTime,endTime,deptCode,EmpNum")] Roster roster)
        {
            var employee = db.employees.Find(roster.EmpNum);
            if (employee.status == "Available")
            {
                //Move the employee to the back of the queue
                var emp = db.employees.Find(roster.EmpNum);
                //increment queue by 1
                emp.queue += 1;
                employee.status = "Not Available";
                db.rosters.Add(roster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Employee has been already assigned";
            }
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName", roster.deptCode);
            ViewBag.EmpNum = new SelectList(db.employees, "EmpNum", "firstName", roster.EmpNum);
            return View(roster);
        }

        // GET: Rosters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roster roster = db.rosters.Find(id);
            if (roster == null)
            {
                return HttpNotFound();
            }
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName", roster.deptCode);
            ViewBag.EmpNum = new SelectList(db.employees, "EmpNum", "firstName", roster.EmpNum);
            return View(roster);
        }

        // POST: Rosters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "code,Date,startTime,endTime,deptCode,EmpNum")] Roster roster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName", roster.deptCode);
            ViewBag.EmpNum = new SelectList(db.employees, "EmpNum", "firstName", roster.EmpNum);
            return View(roster);
        }

        // GET: Rosters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roster roster = db.rosters.Find(id);
            if (roster == null)
            {
                return HttpNotFound();
            }
            return View(roster);
        }

        // POST: Rosters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Roster roster = db.rosters.Find(id);
            var e = roster.EmpNum;
            var employee = db.employees.Find(e);
            employee.status = "Available";
            employee.queue = 1;
            db.rosters.Remove(roster);
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
        //============================= QueueNumber =================================
        public ActionResult QueueNumber(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees emp = db.employees.Find(id);
            return View(new RosterViewModel
            {
                EmpNum = emp.EmpNum,
                queue = emp.queue
            });

        }

        [HttpPost]
        public ActionResult QueueNumber(RosterViewModel r)
        {
            var e = db.employees.Find(r.EmpNum);

            e.EmpNum = r.EmpNum;
            e.queue = r.queue;
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //========================== QueueNumber End================================== 

        public ActionResult AddEmployee()
        {
            Logic l = new Logic();
            //Cleaners
            l.AddEmployeeMonday();
            // l.AddEmployeeWednesday();
            // l.AddEmployeeFriday();

            //Securities
            l.SecurityMondayShift();
            l.SecurityTuesdayShift();
            //l.SecurityWednesdayShift();
            return RedirectToAction("Index");
        }

        /*public ActionResult GetypeOfContracts()
        {
            return Json(db.typeOfContracts.ToList(), JsonRequestBehavior.AllowGet);
           // return Json(lstCat, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployee(string typeCode)
        {
            string n = typeCode;
            //var products = lstProd.Where(p => p.CategoryID == ProductID);
            //return Json(products, JsonRequestBehavior.AllowGet);
            return Json(db.employees.ToList().Where(p => p.typeCode == typeCode), JsonRequestBehavior.AllowGet);
        }*/

        //Search
        public ActionResult Search(string search)
        {
            var roster = db.rosters.Include(r => r.departments).Include(r => r.employees);
            var query = roster.OrderBy(m => m.Date).ToList();

            var Stud = from x in db.rosters select x;
            if (!String.IsNullOrEmpty(search))
            {
                Stud = Stud.Where(y => y.departments.deptName.Contains(search));
                            //|| y.categories.categoryname.Contains(search)
                            //|| y.AccName.Contains(search));
            }
            return View(Stud.Include(r => r.departments).Include(r => r.employees));
        }

    }
}
