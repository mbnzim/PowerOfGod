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
    public class TypeOfIncomesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TypeOfIncomes
        public ActionResult Index()
        {
            return View(db.typeOfIncomes.ToList());
        }

        // GET: TypeOfIncomes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfIncome typeOfIncome = db.typeOfIncomes.Find(id);
            if (typeOfIncome == null)
            {
                return HttpNotFound();
            }
            return View(typeOfIncome);
        }

        // GET: TypeOfIncomes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeOfIncomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,description")] TypeOfIncome typeOfIncome)
        {
            if (ModelState.IsValid)
            {
                db.typeOfIncomes.Add(typeOfIncome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeOfIncome);
        }

        // GET: TypeOfIncomes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfIncome typeOfIncome = db.typeOfIncomes.Find(id);
            if (typeOfIncome == null)
            {
                return HttpNotFound();
            }
            return View(typeOfIncome);
        }

        // POST: TypeOfIncomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,description")] TypeOfIncome typeOfIncome)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeOfIncome).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeOfIncome);
        }

        // GET: TypeOfIncomes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfIncome typeOfIncome = db.typeOfIncomes.Find(id);
            if (typeOfIncome == null)
            {
                return HttpNotFound();
            }
            return View(typeOfIncome);
        }

        // POST: TypeOfIncomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TypeOfIncome typeOfIncome = db.typeOfIncomes.Find(id);
            db.typeOfIncomes.Remove(typeOfIncome);
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
