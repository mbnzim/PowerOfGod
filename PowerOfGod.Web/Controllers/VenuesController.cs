using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Booking;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PowerOfGod.Web.Controllers
{
    public class VenuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Venues
        public async Task<ActionResult> Index()
        {
            var venues = db.venues.Include(v => v.TypeOFVenue);
            return View(await venues.ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = await db.venues.FindAsync(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            ViewBag.VenueCodeID = new SelectList(db.TypeOFVenue, "VenueCodeID", "VenueType");
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VenueID,Venue_Name,Location,Price,Venue_Capacity,VenueCodeID")] Venue venue)
        {
          
            if (ModelState.IsValid)
            {
                db.venues.Add(venue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.VenueCodeID = new SelectList(db.TypeOFVenue, "VenueCodeID", "VenueType", venue.VenueCodeID);
            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = await db.venues.FindAsync(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            ViewBag.VenueCodeID = new SelectList(db.TypeOFVenue, "VenueCodeID", "VenueType", venue.VenueCodeID);
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VenueID,Venue_Name,Image_Url,Location,Price,Venue_Capacity,VenueCodeID")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.VenueCodeID = new SelectList(db.TypeOFVenue, "VenueCodeID", "VenueType", venue.VenueCodeID);
            return View(venue);
        }

        // GET: Venues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = await db.venues.FindAsync(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Venue venue = await db.venues.FindAsync(id);
            db.venues.Remove(venue);
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
