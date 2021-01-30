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
    public class PastorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pastors
        public async Task<ActionResult> Index()
        {
            var pastors = db.pastors.Include(p => p.typeOfPastor);
            return View(await pastors.ToListAsync());
        }

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
        // GET: Pastors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pastor pastor = await db.pastors.FindAsync(id);
            if (pastor == null)
            {
                return HttpNotFound();
            }
            return View(pastor);
        }

        // GET: Pastors/Create
        public ActionResult Create()
        {
            ViewBag.TypeOfPastorID = new SelectList(db.TypeOfpastors, "TypeOfPastorID", "Pastortype");
            return View();
        }

        // POST: Pastors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PastorID,PfirstName,PlastName,BookAmount,Cellnumber,Email,TypeOfPastorID")] Pastor pastor)
        
        {
            if (ModelState.IsValid)
            {

               // upload Picture
                //byte[] data = null;
                //data = new byte[img_upload.ContentLength];
                //img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
                //pastor.Picture= data;

                db.pastors.Add(pastor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TypeOfPastorID = new SelectList(db.TypeOfpastors, "TypeOfPastorID", "Pastortype", pastor.TypeOfPastorID);
            return View(pastor);
        }

        // GET: Pastors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pastor pastor = await db.pastors.FindAsync(id);
            if (pastor == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeOfPastorID = new SelectList(db.TypeOfpastors, "TypeOfPastorID", "Pastortype", pastor.TypeOfPastorID);
            return View(pastor);
        }

        // POST: Pastors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PastorID,PfirstName,PlastName,BookAmount,Cellnumber,Email,TypeOfPastorID")] Pastor pastor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pastor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TypeOfPastorID = new SelectList(db.TypeOfpastors, "TypeOfPastorID", "Pastortype", pastor.TypeOfPastorID);
            return View(pastor);
        }

        // GET: Pastors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pastor pastor = await db.pastors.FindAsync(id);
            if (pastor == null)
            {
                return HttpNotFound();
            }
            return View(pastor);
        }

        // POST: Pastors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pastor pastor = await db.pastors.FindAsync(id);
            db.pastors.Remove(pastor);
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
