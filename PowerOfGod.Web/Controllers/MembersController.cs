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
using PowerOfGod.Domain.Entity.Memberss;
using PowerOfGod.ViewModel.MemberViewModel;

namespace PowerOfGod.Web.Controllers
{
    public class MembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Members
        public ActionResult Index(int? page)
        {
            var employees = db.employees.Include(e => e.contract).Include(e => e.departments);
            var query = db.members.ToList();

            int PageSize = 6;
            int PageNumber = (page ?? 1);
            return View(query.ToPagedList(PageNumber, PageSize));
            //return View(db.members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,FirstName,LastName,IDNumber,gender,UserRole,Email,PhoneNumber,address,Picture")] Members member, HttpPostedFileBase img_upload)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var PasswordHash = new PasswordHasher();
            if (ModelState.IsValid)
            {
                try
                {

                    if (!db.Users.Any(u => u.UserName == member.Email))
                    {
                        //if(!member.Email== )
                        byte[] data = null;
                        data = new byte[img_upload.ContentLength];
                        img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
                        member.UserRole = "Member";
                        member.Picture = data;
                        db.members.Add(member);


                        using (var appcontext = new ApplicationDbContext())
                        {

                            if (!appcontext.Roles.Any(r => r.Name.Equals(member.UserRole)))
                            {
                                var store = new RoleStore<IdentityRole>(appcontext);
                                var manager = new RoleManager<IdentityRole>(store);
                                var role = new IdentityRole { Name = member.UserRole };

                                manager.Create(role);

                                if (!db.Users.Any(u => u.UserName == member.Email))
                                {
                                    var user = new ApplicationUser
                                    {
                                        UserName = member.Email,
                                        Email = member.Email,
                                        EmailConfirmed = true,
                                        PhoneNumber = member.PhoneNumber,
                                        PhoneNumberConfirmed = true,
                                        fullName = member.LastName + " " + member.FirstName,
                                        UserRole = member.UserRole,
                                        gender = member.gender,
                                        IDNumber = member.IDNumber,

                                        PasswordHash = PasswordHash.HashPassword(member.IDNumber.Substring(0, 6)),
                                    };
                                    UserManager.Create(user);
                                    UserManager.AddToRole(user.Id, member.UserRole);
                                }
                            }
                        }
                        if (!db.Users.Any(u => u.UserName == member.Email))
                        {
                            var user = new ApplicationUser
                            {
                                UserName = member.Email,
                                Email = member.Email,
                                EmailConfirmed = true,
                                PhoneNumber = member.PhoneNumber,
                                PhoneNumberConfirmed = true,
                                fullName = member.LastName + " " + member.FirstName,
                                UserRole = member.UserRole,
                                gender = member.gender,
                                IDNumber = member.IDNumber,

                                PasswordHash = PasswordHash.HashPassword(member.IDNumber.Substring(0, 6)),
                            };
                            UserManager.Create(user);
                            UserManager.AddToRole(user.Id, member.UserRole);

                        }
                        db.SaveChanges();
                        TempData["Success"] = member.FirstName + " " + member.LastName + " has successfully been added!";
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ViewBag.errorMessage = "Member with the same email address already exist: ";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Member not added. Error: " + e.Message;
                }
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,FirstName,LastName,IDNumber,gender,UserRole,Email,PhoneNumber,address,Picture")] Members member, HttpPostedFileBase img_upload)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    byte[] data = null;
                    data = new byte[img_upload.ContentLength];
                    img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
                    member.UserRole = "Member";
                    member.Picture = data;
                    db.members.Add(member);

                    db.Entry(member).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("MemberViewModel", "Members");
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Employee not added. Error: " + e.Message;
                }
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Members members = db.members.Find(id);
            db.members.Remove(members);
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

        //=================Member view model======================
        public ActionResult MemberViewModel()
        {
            List<MemberViewModel> vm = new List<MemberViewModel>();
            var list = (from m in db.members
                        join m2 in db.members on m.MemberId equals m2.MemberId

                        select new
                        {
                            m.Email,
                            m.FirstName,
                            m.LastName,
                            m2.gender,
                            m2.UserRole,
                            m2.Picture,
                            m2.PhoneNumber,
                            m2.IDNumber,
                            m2.address,
                            m2.MemberId
                        }).Where(x => x.Email.Equals(User.Identity.Name)).ToList();

            foreach (var item in list)
            {
                MemberViewModel member = new MemberViewModel();
                member.Email = item.Email;
                member.fullname = item.FirstName + " " + item.LastName;
                member.gender = item.gender;
                member.UserRole = item.UserRole;
                member.Picture = item.Picture;
                member.PhoneNumber = item.PhoneNumber;
                member.IDNumber = item.IDNumber;
                member.address = item.address;
                member.MemberId = item.MemberId;
                vm.Add(member);
            }
            return View(vm.ToList());
        }
        //=================//Member view model======================
    }
}
