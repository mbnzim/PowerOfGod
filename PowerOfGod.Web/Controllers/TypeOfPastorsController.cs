using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Booking;

namespace PowerOfGod.Web.Controllers
{
    public class TypeOfPastorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TypeOfPastors
        public async Task<ActionResult> Index()
        {
            return View(await db.TypeOfpastors.ToListAsync());
        }

        // GET: TypeOfPastors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfPastor typeOfPastor = await db.TypeOfpastors.FindAsync(id);
            if (typeOfPastor == null)
            {
                return HttpNotFound();
            }
            return View(typeOfPastor);
        }

        // GET: TypeOfPastors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeOfPastors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TypeOfPastorID,Pastortype")] TypeOfPastor typeOfPastor)
        {
            if (ModelState.IsValid)
            {
                db.TypeOfpastors.Add(typeOfPastor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeOfPastor);
        }

        // GET: TypeOfPastors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfPastor typeOfPastor = await db.TypeOfpastors.FindAsync(id);
            if (typeOfPastor == null)
            {
                return HttpNotFound();
            }
            return View(typeOfPastor);
        }

        // POST: TypeOfPastors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TypeOfPastorID,Pastortype")] TypeOfPastor typeOfPastor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeOfPastor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typeOfPastor);
        }

        // GET: TypeOfPastors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfPastor typeOfPastor = await db.TypeOfpastors.FindAsync(id);
            if (typeOfPastor == null)
            {
                return HttpNotFound();
            }
            return View(typeOfPastor);
        }

        // POST: TypeOfPastors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypeOfPastor typeOfPastor = await db.TypeOfpastors.FindAsync(id);
            db.TypeOfpastors.Remove(typeOfPastor);
            await db.SaveChangesAsync();
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
