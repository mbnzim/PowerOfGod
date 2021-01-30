using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Employee;
using PowerOfGod.Domain.Entity.EmployeLeave;
using PowerOfGod.ViewModel.EmployeeViewModel;

namespace PowerOfGod.Web.Controllers
{
    public class EmployeeLeavelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmployeeLeavels
        public ActionResult Index(int? page)
        {
            var employeeLeavels = db.employeeLeavels.Include(e => e.leavetype);

            var query = db.employeeLeavels.ToList();

            int PageSize = 6;
            int PageNumber = (page ?? 1);

            return View(query.ToPagedList(PageNumber, PageSize));
        }

        // GET: EmployeeLeavels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeLeavel employeeLeavel = db.employeeLeavels.Find(id);
            if (employeeLeavel == null)
            {
                return HttpNotFound();
            }
            return View(employeeLeavel);
        }

        // GET: EmployeeLeavels/Create
        public ActionResult Create()
        {
            ViewBag.leaveTypeID = new SelectList(db.LeaveTypes, "leaveTypeID", "type");
            return View();
        }

        // POST: EmployeeLeavels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaveID,reason,startDate,endDate,numberOfDay,createDate,status,createdBy,updateBy,Picture,leaveTypeID")] EmployeeLeavel employeeLeavel, HttpPostedFileBase img_upload)
        {

            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (ModelState.IsValid)
            {
                try
                {
                    employeeLeavel.User = usermanager.FindByEmail(User.Identity.Name);
                    //upload Picture
                    byte[] data = null;
                    data = new byte[img_upload.ContentLength];
                    img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
                    employeeLeavel.Picture = data;

                    employeeLeavel.numberOfDay = (int)employeeLeavel.LeaveDays();
                    employeeLeavel.createDate = System.DateTime.Now;
                    employeeLeavel.createdBy = employeeLeavel.User.fullName;
                    employeeLeavel.updateBy = employeeLeavel.User.fullName;
                    employeeLeavel.status = "Submitted for Approval";

                    db.employeeLeavels.Add(employeeLeavel);
                    db.SaveChanges();
                    TempData["Success"] = "Leave Request has been successfully created!";
                    return RedirectToAction("LeaveViewModelView", "LeaveModelView");
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Employee not added. Error: " + e.Message;
                }


            }
            ViewBag.leaveTypeID = new SelectList(db.LeaveTypes, "leaveTypeID", "type", employeeLeavel.leaveTypeID);
            return View(employeeLeavel);
        }
        //============================= ApproveLeave =================================
        public ActionResult ApproveLeave(int? id)
        {
            //Roster roster = db.rosters.Find(id);
            EmployeeLeavel employeeLeavel = db.employeeLeavels.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
         
            return View(new ApproveViewModel
            {
                LeaveID = employeeLeavel.LeaveID,
                updateBy = employeeLeavel.updateBy,
                status = employeeLeavel.status
            });     

        }

        [HttpPost]
        public ActionResult ApproveLeave(ApproveViewModel lev)
        {
         
            var emplev = db.employeeLeavels.Find(lev.LeaveID);
            var roster = db.rosters.FirstOrDefault(x => x.Date >= emplev.startDate && x.Date <= emplev.endDate);
          
           // Roster roster = db.rosters.Find(lev.LeaveID);
            emplev.updateBy = "Secretary";
            emplev.status = lev.status;

            if (emplev.status == "Approved" && roster!= null)
            {
                db.rosters.Remove(roster);
                db.SaveChanges();
            }

            db.Entry(emplev).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //========================== ApproveLeave End================================== 


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
             EmployeeLeavel employeeLeavel = db.employeeLeavels.Find(id);
            if (employeeLeavel == null)
            {
                return HttpNotFound();
            }
         
            return View(employeeLeavel);
        }


        // POST: EmployeeLeavels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeLeavel employeeLeavel = db.employeeLeavels.Find(id);
            db.employeeLeavels.Remove(employeeLeavel);
            db.SaveChanges();
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

        //=====================Leave Request Count===========================
        public ActionResult LeaveRequestCount()
        {
            string status = "Submitted for Approval";
            ViewBag.count = (db.employeeLeavels.Where(x => x.status.Equals(status))).Count();
            //(db.contactUs.ToList().Where(x => x.read.Equals(false))).Count();
            return View();
        }
        //=====================/Leave Request Count===========================

    }
}
