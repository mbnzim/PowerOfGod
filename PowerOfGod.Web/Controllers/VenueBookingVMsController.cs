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
    public class VenueBookingVMsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult VenenueBookingAll()
        {
            List<VenueBookingVM> vm = new List<VenueBookingVM>();

            var list = (from vb in db.venueBooking
                        join v in db.venues
                        on vb.VenueID equals v.VenueID
                        select new
                        {
                            vb.VenueBookingID,
                            v.Venue_Name,
                            v.Location,
                            vb.Vstart_date,
                            vb.StartTime,
                            vb.EndTime,
                            v.Venue_Capacity,
                            v.Price,
                            vb.User.Email,
                            vb.Venue_Status

                            // el.createdBy,
                            // el.updateBy
                        }).Where(x => x.Email.Equals(User.Identity.Name)).ToList();

            foreach (var item in list)
            {
                VenueBookingVM lvm = new VenueBookingVM();
                lvm.BookingId = item.VenueBookingID;
                lvm.VenueName = item.Venue_Name;
                lvm.Location = item.Location;
                lvm.Start_date = item.Vstart_date;
                lvm.StartTime = item.StartTime;
                lvm.EndTime = item.EndTime;
                lvm.Price = item.Price;
                lvm.Email= item.Email;
                lvm.V_Status = item.Venue_Status;

                // lvm.updateBy = item.updateBy;
                vm.Add(lvm);
            }
            // var query = vm.ToList();
            // int PageSize = 6;
            // int PageNumber = (page ?? 1);
            return View(vm);
        }











        // GET: VenueBookingVMs
        public async Task<ActionResult> Index()
        {
            return View(await db.venueVM.ToListAsync());
        }

        // GET: VenueBookingVMs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBookingVM venueBookingVM = await db.venueVM.FindAsync(id);
            if (venueBookingVM == null)
            {
                return HttpNotFound();
            }
            return View(venueBookingVM);
        }

        // GET: VenueBookingVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VenueBookingVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookingId,VenueName,Book_Descr,Location,Price,Start_date,StartTime,EndTime")] VenueBookingVM venueBookingVM)
        {
            if (ModelState.IsValid)
            {
                db.venueVM.Add(venueBookingVM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(venueBookingVM);
        }

        // GET: VenueBookingVMs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBookingVM venueBookingVM = await db.venueVM.FindAsync(id);
            if (venueBookingVM == null)
            {
                return HttpNotFound();
            }
            return View(venueBookingVM);
        }

        // POST: VenueBookingVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookingId,VenueName,Book_Descr,Location,Price,Start_date,StartTime,EndTime")] VenueBookingVM venueBookingVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueBookingVM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(venueBookingVM);
        }

        // GET: VenueBookingVMs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBookingVM venueBookingVM = await db.venueVM.FindAsync(id);
            if (venueBookingVM == null)
            {
                return HttpNotFound();
            }
            return View(venueBookingVM);
        }

        // POST: VenueBookingVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VenueBookingVM venueBookingVM = await db.venueVM.FindAsync(id);
            db.venueVM.Remove(venueBookingVM);
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
