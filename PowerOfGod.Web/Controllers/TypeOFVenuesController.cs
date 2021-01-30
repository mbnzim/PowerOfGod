using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PowerOfGod.Domain.Entity.Booking;
using PowerOfGod.Domain.Context;

namespace PowerOfGod.Web.Controllers
{
    public class TypeOFVenuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TypeOFVenues
        public async Task<ActionResult> Index()
        {
            return View(await db.TypeOFVenue.ToListAsync());
        }

        // GET: TypeOFVenues/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOFVenue typeOFVenue = await db.TypeOFVenue.FindAsync(id);
            if (typeOFVenue == null)
            {
                return HttpNotFound();
            }
            return View(typeOFVenue);
        }

        // GET: TypeOFVenues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeOFVenues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VenueCodeID,VenueType")] TypeOFVenue typeOFVenue)
        {
            if (ModelState.IsValid)
            {
                db.TypeOFVenue.Add(typeOFVenue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeOFVenue);
        }

        // GET: TypeOFVenues/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOFVenue typeOFVenue = await db.TypeOFVenue.FindAsync(id);
            if (typeOFVenue == null)
            {
                return HttpNotFound();
            }
            return View(typeOFVenue);
        }

        // POST: TypeOFVenues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VenueCodeID,VenueType")] TypeOFVenue typeOFVenue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeOFVenue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typeOFVenue);
        }

        // GET: TypeOFVenues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOFVenue typeOFVenue = await db.TypeOFVenue.FindAsync(id);
            if (typeOFVenue == null)
            {
                return HttpNotFound();
            }
            return View(typeOFVenue);
        }

        // POST: TypeOFVenues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypeOFVenue typeOFVenue = await db.TypeOFVenue.FindAsync(id);
            db.TypeOFVenue.Remove(typeOFVenue);
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
