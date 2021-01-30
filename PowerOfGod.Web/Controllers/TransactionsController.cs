using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Transactions;

namespace PowerOfGod.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index(int? page)
        {
            var transactions = db.Transactions.Include(t => t.TransactionCode);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.transCode = new SelectList(db.transactionCodes, "transCode", "transTitle");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transId,description,amount,date,transCode,total")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transactions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.transCode = new SelectList(db.transactionCodes, "transCode", "transTitle", transactions.transCode);
            return View(transactions);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            ViewBag.transCode = new SelectList(db.transactionCodes, "transCode", "transTitle", transactions.transCode);
            return View(transactions);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transId,description,amount,date,transCode,total")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.transCode = new SelectList(db.transactionCodes, "transCode", "transTitle", transactions.transCode);
            return View(transactions);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            db.Transactions.Remove(transactions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult FinanceView(DateTime startdate, DateTime enddate)
        {
            var transaction = new Transactions();
            var expense = new Church_Expenses();
            ViewBag.SumOfExpenses = expense.SumOfExpenses();
            ViewBag.Income = transaction.Income();

            var trans = from x in db.Transactions
                        select x;
            //if (Session != null)
            //{
            //if (startdate != null && enddate != null)
            //{

            return View(db.Transactions.ToList().Where(x => (startdate <= x.date) && (enddate >= x.date)).ToList());
        }
        public ActionResult GetData()
        {
            //int expense = db.Transactions.Where(x => x.description == "Expense").Count();
            //int income = db.Transactions.Where(x => x.description == "Income").Count();
            double expense = db.Transactions.Where(x => x.transCode == "EXP101").Sum(x => x.amount);
            double income = db.Transactions.Where(x => x.transCode == "INC101").Sum(x => x.amount);


            Ratio obj = new Ratio();
            obj.Expense = expense;
            obj.Income = income;


            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public double Expense { get; set; }
            public double Income { get; set; }

        }

        public ActionResult Report()
        {
            return View();
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
