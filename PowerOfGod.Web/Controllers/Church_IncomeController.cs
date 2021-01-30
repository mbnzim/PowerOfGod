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
    public class Church_IncomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Church_Income
        public ActionResult Index()
        {
            var church_Incomes = db.church_Incomes.Include(c => c.TypeOfIncomes);
            return View(church_Incomes.ToList());
        }

        // GET: Church_Income/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Church_Income church_Income = db.church_Incomes.Find(id);
            if (church_Income == null)
            {
                return HttpNotFound();
            }
            return View(church_Income);
        }

        // GET: Church_Income/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.typeOfIncomes, "Id", "description");
            return View();
        }

        // POST: Church_Income/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpensesId,Amount,date,Id")] Church_Income church_Income)
        {

            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var desc = db.typeOfIncomes.Find(church_Income.Id);
            var transaction = new Transactions()
            {


                //transId = 1,
                date = church_Income.date,
                amount = church_Income.Amount,
                transCode = "INC102",
                description = desc.description
            };
            db.Transactions.Add(transaction);
            db.SaveChanges();

            if (ModelState.IsValid)
            {
                db.church_Incomes.Add(church_Income);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.typeOfIncomes, "Id", "description", church_Income.Id);
            return View(church_Income);
        }

        // GET: Church_Income/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Church_Income church_Income = db.church_Incomes.Find(id);
            if (church_Income == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.typeOfIncomes, "Id", "description", church_Income.Id);
            return View(church_Income);
        }

        // POST: Church_Income/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpensesId,Amount,date,Id")] Church_Income church_Income)
        {
            if (ModelState.IsValid)
            {
                db.Entry(church_Income).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.typeOfIncomes, "Id", "description", church_Income.Id);
            return View(church_Income);
        }

        // GET: Church_Income/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Church_Income church_Income = db.church_Incomes.Find(id);
            if (church_Income == null)
            {
                return HttpNotFound();
            }
            return View(church_Income);
        }

        // POST: Church_Income/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Church_Income church_Income = db.church_Incomes.Find(id);
            db.church_Incomes.Remove(church_Income);
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
