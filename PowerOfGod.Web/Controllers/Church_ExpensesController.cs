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
using PowerOfGod.Domain.Entity.Transactions;

namespace PowerOfGod.Web.Controllers
{
    public class Church_ExpensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Church_Expenses
        public ActionResult Index()
        {
            var church_Expenses = db.Church_Expenses.Include(c => c.TypeOfExpense_Incomes);
            return View(church_Expenses.ToList());
        }

        // GET: Church_Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Church_Expenses church_Expenses = db.Church_Expenses.Find(id);
            if (church_Expenses == null)
            {
                return HttpNotFound();
            }
            return View(church_Expenses);
        }

        // GET: Church_Expenses/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.typeOfExpense_Incomes, "Id", "description");
            return View();
        }

        // POST: Church_Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpensesId,Amount,date,Id,total")] Church_Expenses church_Expenses)
        {



            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var desc = db.typeOfExpense_Incomes.Find(church_Expenses.Id);
            var transaction = new Transactions()
            {


                //transId = 1,
                date = church_Expenses.date,
                amount = church_Expenses.Amount,
                transCode = "EXP101",
                description = desc.description
            };
            db.Transactions.Add(transaction);
            db.SaveChanges();


            if (ModelState.IsValid)
            { 
                db.Church_Expenses.Add(church_Expenses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.typeOfExpense_Incomes, "Id", "description", church_Expenses.Id);
            return View(church_Expenses);
        }
        // GET: Church_Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Church_Expenses church_Expenses = db.Church_Expenses.Find(id);
            if (church_Expenses == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.typeOfExpense_Incomes, "Id", "description", church_Expenses.Id);
            return View(church_Expenses);
        }

        // POST: Church_Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpensesId,Amount,date,Id,total")] Church_Expenses church_Expenses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(church_Expenses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.typeOfExpense_Incomes, "Id", "description", church_Expenses.Id);
            return View(church_Expenses);
        }

        // GET: Church_Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Church_Expenses church_Expenses = db.Church_Expenses.Find(id);
            if (church_Expenses == null)
            {
                return HttpNotFound();
            }
            return View(church_Expenses);
        }

        // POST: Church_Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Church_Expenses church_Expenses = db.Church_Expenses.Find(id);
            db.Church_Expenses.Remove(church_Expenses);
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
