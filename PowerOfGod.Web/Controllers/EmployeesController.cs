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
using PowerOfGod.ViewModel.EmployeeViewModel;

namespace PowerOfGod.Web.Controllers
{
    [Authorize(Roles = "SupperAdmin,Administrator")]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index(int? page)
        {
            var employees = db.employees.Include(e => e.contract).Include(e => e.departments);
            var query = employees.OrderBy(m => m.hireDate).ToList();

            int PageSize = 6;
            int PageNumber = (page ?? 1);
            return View(query.ToPagedList(PageNumber, PageSize));
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.typeCode = new SelectList(db.typeOfContracts, "typeCode", "typeName");
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpNum,firstName,lastName,IDNumber,gender,UserRole,hireDate,mobile,email,status,queue,deptCode,typeCode,Picture,shift")] Employees employees, HttpPostedFileBase img_upload)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var PasswordHash = new PasswordHasher();
            if (ModelState.IsValid)
            {
                try
                {
                    //upload Picture
                    byte[] data = null;
                    data = new byte[img_upload.ContentLength];
                    img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
                    employees.Picture = data;

                    employees.queue = 1;
                    employees.hireDate = System.DateTime.Now;
                    employees.status = "Available";
                    db.employees.Add(employees);

                    using (var appcontext = new ApplicationDbContext())
                    {

                        if (!appcontext.Roles.Any(r => r.Name.Equals(employees.UserRole)))
                        {
                            var store = new RoleStore<IdentityRole>(appcontext);
                            var manager = new RoleManager<IdentityRole>(store);
                            var role = new IdentityRole { Name = employees.UserRole };

                            manager.Create(role);

                            if (!db.Users.Any(u => u.UserName == employees.EmpNum))
                            {
                                var user = new ApplicationUser
                                {
                                    UserName = employees.email,
                                    Email = employees.email,
                                    EmailConfirmed = true,
                                    PhoneNumber = employees.mobile,
                                    PhoneNumberConfirmed = true,
                                    fullName = employees.lastName + " " + employees.firstName,
                                    UserRole = employees.UserRole,
                                    gender = employees.gender,
                                    IDNumber = employees.IDNumber,

                                    PasswordHash = PasswordHash.HashPassword(employees.IDNumber.Substring(0, 6)),
                                };
                                UserManager.Create(user);
                                UserManager.AddToRole(user.Id, employees.UserRole);
                            }
                        }
                    }
                    if (!db.Users.Any(u => u.UserName == employees.EmpNum))
                    {
                        var user = new ApplicationUser
                        {
                            UserName = employees.email,
                            Email = employees.email,
                            EmailConfirmed = true,
                            PhoneNumber = employees.mobile,
                            PhoneNumberConfirmed = true,
                            fullName = employees.lastName + " " + employees.firstName,
                            UserRole = employees.UserRole,
                            gender = employees.gender,
                            IDNumber = employees.IDNumber,

                            PasswordHash = PasswordHash.HashPassword(employees.IDNumber.Substring(0, 6)),
                        };
                        UserManager.Create(user);
                        UserManager.AddToRole(user.Id, employees.UserRole);

                    }
                    db.SaveChanges();
                    TempData["Success"] = employees.firstName + " " + employees.lastName + " has successfully been added!";
                    return RedirectToAction("Index");

                    //return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Employee not added. Error: " + e.Message;
                }

            }
            ViewBag.typeCode = new SelectList(db.typeOfContracts, "typeCode", "typeName", employees.typeCode);
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName", employees.deptCode);
            //TempData["Success"] = employees.firstName + " " + employees.lastName + " has successfully been added!";
            ModelState.Clear();
            return View(employees);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.typeCode = new SelectList(db.typeOfContracts, "typeCode", "typeName", employees.typeCode);
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName", employees.deptCode);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpNum,firstName,lastName,IDNumber,gender,UserRole,hireDate,mobile,email,status,queue,deptCode,typeCode,Picture,shift")] Employees employees, HttpPostedFileBase img_upload)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //upload Picture
                    byte[] data = null;
                    data = new byte[img_upload.ContentLength];
                    img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
                    employees.Picture = data;

                    employees.queue = 1;
                    employees.hireDate = System.DateTime.Now;
                    employees.status = "Available";
                    db.Entry(employees).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Employee not added. Error: " + e.Message;
                }

            }
            ViewBag.typeCode = new SelectList(db.typeOfContracts, "typeCode", "typeName", employees.typeCode);
            ViewBag.deptCode = new SelectList(db.departments, "deptCode", "deptName", employees.deptCode);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employees employees = db.employees.Find(id);
            db.employees.Remove(employees);
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
        //============================= QueueNumber =================================
        public ActionResult QueueNumber(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees emp = db.employees.Find(id);
            return View(new RosterViewModel
            {
                EmpNum = emp.EmpNum,
                queue = emp.queue
            });

        }

        [HttpPost]
        public ActionResult QueueNumber(RosterViewModel r)
        {
            var e = db.employees.Find(r.EmpNum);

            e.EmpNum = r.EmpNum;
            e.queue = r.queue;
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //========================== QueueNumber End================================== 

        public ActionResult SearchEmplyee(string search)
        {
            var employees = db.employees.Include(e => e.contract).Include(e => e.departments);
            var query = employees.OrderBy(m => m.hireDate).ToList();

            var Stud = from x in db.employees select x;
            if (!String.IsNullOrEmpty(search))
            {
                Stud = Stud.Where(y => y.departments.deptName.Contains(search));
                //|| y.categories.categoryname.Contains(search)
                //|| y.AccName.Contains(search));
            }
            return View(Stud.Include(e => e.contract).Include(e => e.departments));
        }
    }
}
