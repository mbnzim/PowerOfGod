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
using Microsoft.AspNet.Identity;
using PowerOfGod.Domain.Context;
using PowerOfGod.ViewModel.BookingAproval;
using System.Text;
using PowerOfGod.Domain.Entity.Transactions;

namespace PowerOfGod.Web.Controllers
{
    public class VenueBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VenueBookings
        public async Task<ActionResult> BookingIndexVenue()
        {
            var venueBooking = db.venueBooking.Include(v => v.Venue).Include(v => v.VenueBookingDescription);
            return View(await venueBooking.ToListAsync());
        }
        public async Task<ActionResult> Index()
        {
            var venueBooking = db.venueBooking.Include(v => v.Venue).Include(v => v.VenueBookingDescription);
            return View(await venueBooking.ToListAsync());
        }

        // GET: VenueBookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBooking venueBooking = await db.venueBooking.FindAsync(id);
            if (venueBooking == null)
            {
                return HttpNotFound();
            }
            return View(venueBooking);
        }

        // GET: VenueBookings/Create
        public ActionResult Create()
        {
            ViewBag.VenueID = new SelectList(db.venues, "VenueID", "Venue_Name");
            ViewBag.DescriptionID = new SelectList(db.venueBookingDescription, "DescriptionID", "booking_Description");
            return View();
        }

        // POST: VenueBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VenueBookingID,Vstart_date,StartTime,EndTime,Venue_Status,UserId,DescriptionID,VenueID,BookedBy,Email,Venue_Status")] VenueBooking venueBooking)
        {

            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db));

            //Venue venue = new Venue();
            //venue = db.venues.Find(venueBooking.VenueID);
            ////pastorBooking.BookingAmount = pastor.BookAmount;
            //var desc = db.venueBookingDescription.Find(venueBooking.VenueBookingDescription);
            //var transaction = new Transactions()
            //{

            //    //transId = 1,
            //    date = venueBooking.Vstart_date,
            //    // pastorBooking.BookingAmount = pastor.BookAmount,
            //    amount = venue.Price,
            //    transCode = "BOOK102",
            //    description = desc.booking_Description,
            //};
            //db.Transactions.Add(transaction);
            //db.SaveChanges();

            int result = DateTime.Compare(DateTime.Now, venueBooking.Vstart_date);
            DateTime sDate = db.venueBooking.Where(p => p.Vstart_date == venueBooking.Vstart_date).Select(p => p.Vstart_date).FirstOrDefault();



            if (ModelState.IsValid == true && venueBooking.CheckedDate() == true && (venueBooking.Vstart_date == sDate) == false)
            {
                var venue = db.venues.Find(venueBooking.VenueID);
                venueBooking.V_Price = venue.Price;
                venueBooking.Venue_Status = "Waiting for Approval";
                string Amount = venueBooking.V_Price.ToString();
                venueBooking.User = usermanager.FindByEmail(User.Identity.Name);
                venueBooking.BookedBy = venueBooking.User.fullName;
                db.venueBooking.Add(venueBooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Payfast", new { Amount, venueBooking.VenueBookingID, venueBooking.DescriptionID });
            }

            ViewBag.VenueID = new SelectList(db.venues, "VenueID", "Venue_Name", venueBooking.VenueID);
            ViewBag.DescriptionID = new SelectList(db.venueBookingDescription, "DescriptionID", "booking_Description", venueBooking.DescriptionID);
            return View(venueBooking);
        }

        //============================= ApproveBooking =================================
        // GET: PastorBookings/Edit/5
        public ActionResult ApproveVenueBooking(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBooking venueBookings = db.venueBooking.Find(id);
            return View(new ApprovalVenueVM
            {
                VenueBookingID = venueBookings.VenueBookingID,
                //  updateBy = e.updateBy,
                Venue_Status = venueBookings.Venue_Status,
            });
        }
        [HttpPost]
        public ActionResult ApproveVenueBooking(ApprovalVenueVM approveVenue)
        {
            var BookVenue = db.venueBooking.Find(approveVenue.VenueBookingID);

            // BookPastor.updateBy = "Pastor";
            BookVenue.Venue_Status = approveVenue.Venue_Status;
            db.Entry(BookVenue).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // ========================== ApproveBooking End================================== 

        public async Task<ActionResult> SuccesFullVenueBooking()
        {

            return View();
        }

        //=====================================================Payment========================================

        public ActionResult PayFast(string Amount)
        {
            // Create the order in your DB and get the ID
            string V_Price = Amount;
            string VenueBookingID = new Random().Next(1, 9999).ToString();
            string name = "Booking Ref#" + VenueBookingID;
            string description = "Venue Bookings";

            string site = "";
            string merchant_id = "";
            string merchant_key = "";

            // Check if we are using the test or live system
            string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

            if (paymentMode == "test")
            {
                site = "https://sandbox.payfast.co.za/eng/process?";
                merchant_id = "10005631";
                merchant_key = "znbndc034nb6b";
            }
            else
            {
                throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
            }
            // Build the query string for payment site

            System.Text.StringBuilder str = new StringBuilder();
            str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
            str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
            str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
            str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
            // str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));
            str.Append("&m_payment_id=" + HttpUtility.UrlEncode(VenueBookingID));
            str.Append("&amount=" + HttpUtility.UrlEncode(V_Price));
            str.Append("&item_name=" + HttpUtility.UrlEncode(name));
            str.Append("&item_description=" + HttpUtility.UrlEncode(description));

            // Redirect to PayFast
            Response.Redirect(site + str.ToString());

            return View();
        }

        public ActionResult Success(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBooking venueBooking = db.venueBooking.Find(id);
            if (venueBooking == null)
            {
                return HttpNotFound();
            }
            return View(venueBooking);

        }
        public ActionResult Acknowledgement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBooking venueBooking = db.venueBooking.Find(id);
            if (venueBooking == null)
            {
                return HttpNotFound();
            }
            return View(venueBooking);
        }

        //======================================== Payment ================================================================



        // GET: VenueBookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBooking venueBooking = await db.venueBooking.FindAsync(id);
            if (venueBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.VenueID = new SelectList(db.venues, "VenueID", "Venue_Name", venueBooking.VenueID);
            ViewBag.DescriptionID = new SelectList(db.venueBookingDescription, "DescriptionID", "booking_Description", venueBooking.DescriptionID);
            return View(venueBooking);
        }

        // POST: VenueBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VenueBookingID,Vstart_date,StartTime,EndTime,Venue_Status,ReserveVenue,UserId,DescriptionID,VenueID,NumberInUse")] VenueBooking venueBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueBooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.VenueID = new SelectList(db.venues, "VenueID", "Venue_Name", venueBooking.VenueID);
            ViewBag.DescriptionID = new SelectList(db.venueBookingDescription, "DescriptionID", "booking_Description", venueBooking.DescriptionID);
            return View(venueBooking);
        }

        // GET: VenueBookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBooking venueBooking = await db.venueBooking.FindAsync(id);
            if (venueBooking == null)
            {
                return HttpNotFound();
            }
            return View(venueBooking);
        }

        // POST: VenueBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VenueBooking venueBooking = await db.venueBooking.FindAsync(id);
            db.venueBooking.Remove(venueBooking);
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
