using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Booking;
using System.Text;
using PowerOfGod.ViewModel.BookingAproval;
using PowerOfGod.Domain.Entity.Transactions;

namespace PowerOfGod.Web.Controllers
{
    public class PastorBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: PastorBookings
        public async Task<ActionResult> Index()
        {
            var pastorsBooking = db.pastorsBooking.Include(p => p.Pastor).Include(p => p.PastorBookingReason);
            return View(await pastorsBooking.ToListAsync());
        }
        public async Task<ActionResult> BookingIndexUser()
        {
            var pastorsBooking = db.pastorsBooking.Include(p => p.Pastor).Include(p => p.PastorBookingReason);
            return View(await pastorsBooking.ToListAsync());
        }

        //===================================================Cascading =============================
        public ActionResult GetPastorTypes()
        {
            return Json(db.TypeOfpastors.ToList(), JsonRequestBehavior.AllowGet);
            //return Json(typeList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPastor(int TypeOfPastorID)
        {
            //var pastor = pastorList.Where(p => p.TypeOfPastorID == PastorID);
            // return Json(pastor, JsonRequestBehavior.AllowGet);
            return Json(db.pastors.ToList().Where(p => p.TypeOfPastorID == TypeOfPastorID), JsonRequestBehavior.AllowGet);
        }
        //===================================================Cascading =============================

        // GET: PastorBookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBooking pastorBooking = await db.pastorsBooking.FindAsync(id);
            if (pastorBooking == null)
            {
                return HttpNotFound();
            }
            return View(pastorBooking);
        }

        // GET: PastorBookings/Create
        public ActionResult Create()
        {
            ViewBag.PastorID = new SelectList(db.pastors, "PastorID", "PfirstName");
            ViewBag.ReasonID = new SelectList(db.bookingReason, "ReasonID", "Reason");
            return View();
        }

        // POST: PastorBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.




    [HttpPost]
    [ValidateAntiForgeryToken]
        
    public async Task<ActionResult> Create([Bind(Include = "PastorBookingID,Start_date,TimeStart,TimeEnd,UserId,PastorID,ReasonID,NumberInUse,BookedBy,BookingAmount,Status")] PastorBooking pastorBooking)
        {
            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            Pastor pastor = new Pastor();
            pastor = db.pastors.Find(pastorBooking.PastorID);
            //pastorBooking.BookingAmount = pastor.BookAmount;

            var desc = db.bookingReason.Find(pastorBooking.ReasonID);
            var transaction = new Transactions()
            {

                //transId = 1,
                date = pastorBooking.Start_date,
               // pastorBooking.BookingAmount = pastor.BookAmount,
                amount = pastor.BookAmount,
                transCode = "BOOK101",
                description = desc.Reason
            };
            db.Transactions.Add(transaction);
            db.SaveChanges();

            //UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            int result = DateTime.Compare(DateTime.Now, pastorBooking.Start_date);
            DateTime sDate = db.pastorsBooking.Where(p => p.Start_date == pastorBooking.Start_date).Select(p => p.Start_date).FirstOrDefault();
            //if (ModelState.IsValid == true && (pastorBooking.Start_date == sDate) == true)
            //{
            //    TempData["Message"] = "Date is booked";
            //    return View();
            //}
            if (ModelState.IsValid == true && pastorBooking.CheckDate() == true && (pastorBooking.Start_date == sDate) == false)
            {

                 pastor = db.pastors.Find(pastorBooking.PastorID);
                pastorBooking.BookingAmount = pastor.BookAmount;
                string Amount = pastorBooking.BookingAmount.ToString();
                pastorBooking.Status = "Waiting for Approval";

                pastorBooking.User = usermanager.FindByEmail(User.Identity.Name);
                pastorBooking.BookedBy = pastorBooking.User.fullName;
                db.pastorsBooking.Add(pastorBooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Payfast",new { Amount,pastorBooking.PastorBookingID,pastorBooking.ReasonID});
            }
        

            ViewBag.PastorID = new SelectList(db.pastors, "PastorID", "PfirstName", pastorBooking.PastorID);
            ViewBag.ReasonID = new SelectList(db.bookingReason, "ReasonID", "Reason", pastorBooking.ReasonID);
            ViewBag.Message = "The selected Date is already picked, please choose another Date!!!";
            return View(pastorBooking);
        }

        // GET: PastorBookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBooking pastorBooking = await db.pastorsBooking.FindAsync(id);
            if (pastorBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.PastorID = new SelectList(db.pastors, "PastorID", "PfirstName", pastorBooking.PastorID);
            ViewBag.ReasonID = new SelectList(db.bookingReason, "ReasonID", "Reason", pastorBooking.ReasonID);
            return View(pastorBooking);
        }

        //=====================================================Payment========================================

        public ActionResult PayFast(string Amount)
        {
            // Create the order in your DB and get the ID
            // Create the order in your DB and get the ID
            string BookAmount = Amount;
            string PastorBookingID = new Random().Next(1, 9999).ToString();
            string name = "Booking Ref#" + PastorBookingID;
            string description = "Pastors Bookings";

            string site = "https://sandbox.payfast.co.za/eng/process?";
            string merchant_id = "";
            string merchant_key = "";

            // Check if we are using the test or live system
            string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

            if (paymentMode == "test")
            {
                site = "https://sandbox.payfast.co.za/eng/process?";
                merchant_id = "10000100";
                merchant_key = "46f0cd694581a";
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
            //str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));


            str.Append("&m_payment_id=" + HttpUtility.UrlEncode(PastorBookingID));
            str.Append("&amount=" + HttpUtility.UrlEncode(BookAmount));
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
            PastorBooking pastorBooking = db.pastorsBooking.Find(id);
            if (pastorBooking == null)
            {
                return HttpNotFound();
            }
            return View(pastorBooking);

        }
        public ActionResult Acknowledgement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBooking pastorBooking = db.pastorsBooking.Find(id);
            if (pastorBooking == null)
            {
                return HttpNotFound();
            }
            return View(pastorBooking);
        }

        //============================= ApproveBooking =================================
        // GET: PastorBookings/Edit/5
        public ActionResult ApproveBooking(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBooking pastorBookings = db.pastorsBooking.Find(id);
            return View(new ApprovalVM
            {
                PastorBookingID = pastorBookings.PastorBookingID,
                //  updateBy = e.updateBy,
                Status = pastorBookings.Status
            });
        }
        [HttpPost]
        public ActionResult ApproveBooking(ApprovalVM approve)
        {
            var BookPastor = db.pastorsBooking.Find(approve.PastorBookingID);

            // BookPastor.updateBy = "Pastor";
            BookPastor.Status = approve.Status;
            db.Entry(BookPastor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // ========================== ApproveBooking End================================== 

        public async Task<ActionResult> SuccesFullIBooking()
        {

            return View();
        }

        // POST: PastorBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PastorBookingID,Start_date,TimeStart,TimeEnd,Reserved,UserId,PastorID,ReasonID,NumberInUse")] PastorBooking pastorBooking)
        {
           
            if (ModelState.IsValid)
            {
               
                db.Entry(pastorBooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PastorID = new SelectList(db.pastors, "PastorID", "PfirstName", pastorBooking.PastorID);
            ViewBag.ReasonID = new SelectList(db.bookingReason, "ReasonID", "Reason", pastorBooking.ReasonID);
            return View(pastorBooking);
        }

        // GET: PastorBookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBooking pastorBooking = await db.pastorsBooking.FindAsync(id);
            if (pastorBooking == null)
            {
                return HttpNotFound();
            }
            return View(pastorBooking);
        }

        // POST: PastorBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PastorBooking pastorBooking = await db.pastorsBooking.FindAsync(id);
            db.pastorsBooking.Remove(pastorBooking);
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
