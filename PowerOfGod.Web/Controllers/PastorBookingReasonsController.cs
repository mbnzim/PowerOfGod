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
    public class PastorBookingReasonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PastorBookingReasons
        public async Task<ActionResult> Index()
        {
            return View(await db.bookingReason.ToListAsync());
        }

        // GET: PastorBookingReasons/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBookingReason pastorBookingReason = await db.bookingReason.FindAsync(id);
            if (pastorBookingReason == null)
            {
                return HttpNotFound();
            }
            return View(pastorBookingReason);
        }

        // GET: PastorBookingReasons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PastorBookingReasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReasonID,Reason")] PastorBookingReason pastorBookingReason)
        {
            if (ModelState.IsValid)
            {
                db.bookingReason.Add(pastorBookingReason);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pastorBookingReason);
        }

        // GET: PastorBookingReasons/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBookingReason pastorBookingReason = await db.bookingReason.FindAsync(id);
            if (pastorBookingReason == null)
            {
                return HttpNotFound();
            }
            return View(pastorBookingReason);
        }

        // POST: PastorBookingReasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReasonID,Reason")] PastorBookingReason pastorBookingReason)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pastorBookingReason).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pastorBookingReason);
        }

        // GET: PastorBookingReasons/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBookingReason pastorBookingReason = await db.bookingReason.FindAsync(id);
            if (pastorBookingReason == null)
            {
                return HttpNotFound();
            }
            return View(pastorBookingReason);
        }

        // POST: PastorBookingReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PastorBookingReason pastorBookingReason = await db.bookingReason.FindAsync(id);
            db.bookingReason.Remove(pastorBookingReason);
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
