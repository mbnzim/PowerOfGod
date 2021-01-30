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
    public class TypeOfExpense_IncomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TypeOfExpense_Income
        public ActionResult Index()
        {
            return View(db.typeOfExpense_Incomes.ToList());
        }

        // GET: TypeOfExpense_Income/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfExpense_Income typeOfExpense_Income = db.typeOfExpense_Incomes.Find(id);
            if (typeOfExpense_Income == null)
            {
                return HttpNotFound();
            }
            return View(typeOfExpense_Income);
        }

        // GET: TypeOfExpense_Income/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeOfExpense_Income/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,description")] TypeOfExpense_Income typeOfExpense_Income)
        {
            if (ModelState.IsValid)
            {
                db.typeOfExpense_Incomes.Add(typeOfExpense_Income);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeOfExpense_Income);
        }

        // GET: TypeOfExpense_Income/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfExpense_Income typeOfExpense_Income = db.typeOfExpense_Incomes.Find(id);
            if (typeOfExpense_Income == null)
            {
                return HttpNotFound();
            }
            return View(typeOfExpense_Income);
        }

        // POST: TypeOfExpense_Income/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,description")] TypeOfExpense_Income typeOfExpense_Income)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeOfExpense_Income).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeOfExpense_Income);
        }

        // GET: TypeOfExpense_Income/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfExpense_Income typeOfExpense_Income = db.typeOfExpense_Incomes.Find(id);
            if (typeOfExpense_Income == null)
            {
                return HttpNotFound();
            }
            return View(typeOfExpense_Income);
        }

        // POST: TypeOfExpense_Income/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TypeOfExpense_Income typeOfExpense_Income = db.typeOfExpense_Incomes.Find(id);
            db.typeOfExpense_Incomes.Remove(typeOfExpense_Income);
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
