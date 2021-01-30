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
using PagedList;

namespace PowerOfGod.Web.Controllers
{
    public class PastorBookingViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult ViewModelAction()
        {
            List<PastorBookingViewModel> vm = new List<PastorBookingViewModel>();

            var list = (from el in db.pastorsBooking
                        join l in db.pastors
                        on el.PastorID equals l.PastorID
                        select new
                        {
                            el.PastorBookingID,
                            l.PfirstName,
                            l.PlastName,
                            el.Start_date,
                            el.TimeStart,
                            el.TimeEnd,
                            l.BookAmount,
                            el.User.Email,
                            el.Status

                            // el.createdBy,
                            // el.updateBy
                        }).Where(x => x.Email.Equals(User.Identity.Name)).ToList();

            foreach (var item in list)
            {
                PastorBookingViewModel lvm = new PastorBookingViewModel();
                lvm.PastorBookingVMId = item.PastorBookingID;
                lvm.PFirstName = item.PfirstName;
                lvm.PLastName = item.PlastName;
                lvm.Start_date = item.Start_date;
                lvm.TimeStart = item.TimeStart;
                lvm.TimeEnd = item.TimeEnd;
                lvm.BookAmount = item.BookAmount;
                lvm.Email = item.Email;
                lvm.Status = item.Status;

                // lvm.updateBy = item.updateBy;
                vm.Add(lvm);
            }
            // var query = vm.ToList();
            // int PageSize = 6;
            // int PageNumber = (page ?? 1);
            return View(vm);
        }



        // GET: PastorBookingViewModels
        public async Task<ActionResult> Index()
        {
            return View(await db.PastorVM.ToListAsync());
        }

        // GET: PastorBookingViewModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBookingViewModel pastorBookingViewModel = await db.PastorVM.FindAsync(id);
            if (pastorBookingViewModel == null)
            {
                return HttpNotFound();
            }
            return View(pastorBookingViewModel);
        }

        // GET: PastorBookingViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PastorBookingViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PastorBookingVMId,PFirstName,PLastName,Description,Start_date,TimeStart,TimeEnd,BookAmount")] PastorBookingViewModel pastorBookingViewModel)
        {
            if (ModelState.IsValid)
            {
                db.PastorVM.Add(pastorBookingViewModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pastorBookingViewModel);
        }

        // GET: PastorBookingViewModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBookingViewModel pastorBookingViewModel = await db.PastorVM.FindAsync(id);
            if (pastorBookingViewModel == null)
            {
                return HttpNotFound();
            }
            return View(pastorBookingViewModel);
        }

        // POST: PastorBookingViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PastorBookingVMId,PFirstName,PLastName,Description,Start_date,TimeStart,TimeEnd,BookAmount")] PastorBookingViewModel pastorBookingViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pastorBookingViewModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pastorBookingViewModel);
        }

        // GET: PastorBookingViewModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastorBookingViewModel pastorBookingViewModel = await db.PastorVM.FindAsync(id);
            if (pastorBookingViewModel == null)
            {
                return HttpNotFound();
            }
            return View(pastorBookingViewModel);
        }

        // POST: PastorBookingViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PastorBookingViewModel pastorBookingViewModel = await db.PastorVM.FindAsync(id);
            db.PastorVM.Remove(pastorBookingViewModel);
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
