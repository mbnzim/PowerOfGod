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
    public class VenueBookingDescriptionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VenueBookingDescriptions
        public async Task<ActionResult> Index()
        {
            return View(await db.venueBookingDescription.ToListAsync());
        }

        // GET: VenueBookingDescriptions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBookingDescription venueBookingDescription = await db.venueBookingDescription.FindAsync(id);
            if (venueBookingDescription == null)
            {
                return HttpNotFound();
            }
            return View(venueBookingDescription);
        }

        // GET: VenueBookingDescriptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VenueBookingDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DescriptionID,booking_Description")] VenueBookingDescription venueBookingDescription)
        {
            if (ModelState.IsValid)
            {
                db.venueBookingDescription.Add(venueBookingDescription);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(venueBookingDescription);
        }

        // GET: VenueBookingDescriptions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBookingDescription venueBookingDescription = await db.venueBookingDescription.FindAsync(id);
            if (venueBookingDescription == null)
            {
                return HttpNotFound();
            }
            return View(venueBookingDescription);
        }

        // POST: VenueBookingDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DescriptionID,booking_Description")] VenueBookingDescription venueBookingDescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueBookingDescription).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(venueBookingDescription);
        }

        // GET: VenueBookingDescriptions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBookingDescription venueBookingDescription = await db.venueBookingDescription.FindAsync(id);
            if (venueBookingDescription == null)
            {
                return HttpNotFound();
            }
            return View(venueBookingDescription);
        }

        // POST: VenueBookingDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VenueBookingDescription venueBookingDescription = await db.venueBookingDescription.FindAsync(id);
            db.venueBookingDescription.Remove(venueBookingDescription);
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
