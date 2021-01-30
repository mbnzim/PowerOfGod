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
    public class TransactionCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TransactionCodes
        public ActionResult Index()
        {
            return View(db.transactionCodes.ToList());
        }

        // GET: TransactionCodes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionCode transactionCode = db.transactionCodes.Find(id);
            if (transactionCode == null)
            {
                return HttpNotFound();
            }
            return View(transactionCode);
        }

        // GET: TransactionCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transCode,transTitle")] TransactionCode transactionCode)
        {
            if (ModelState.IsValid)
            {
                db.transactionCodes.Add(transactionCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transactionCode);
        }

        // GET: TransactionCodes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionCode transactionCode = db.transactionCodes.Find(id);
            if (transactionCode == null)
            {
                return HttpNotFound();
            }
            return View(transactionCode);
        }

        // POST: TransactionCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transCode,transTitle")] TransactionCode transactionCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactionCode);
        }

        // GET: TransactionCodes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionCode transactionCode = db.transactionCodes.Find(id);
            if (transactionCode == null)
            {
                return HttpNotFound();
            }
            return View(transactionCode);
        }

        // POST: TransactionCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TransactionCode transactionCode = db.transactionCodes.Find(id);
            db.transactionCodes.Remove(transactionCode);
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
