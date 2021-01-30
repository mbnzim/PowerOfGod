using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.UserContact;
using PowerOfGod.ViewModel.EmployeeViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerOfGod.Web.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //=================Employee view model======================
        public ActionResult ViewModelAction()
        {
            List<EmployeeViewModel> vm = new List<EmployeeViewModel>();
            var list = (from e in db.employees
                        join d in db.departments on e.deptCode equals d.deptCode
                        join t in db.typeOfContracts on e.typeCode equals t.typeCode
                        select new
                        {
                            e.EmpNum,
                            e.email,
                            e.firstName,
                            e.lastName,
                            e.gender,
                            e.UserRole,
                            e.Picture,
                            d.deptName,
                            t.typeName
                        }).Where(x => x.email.Equals(User.Identity.Name)).ToList();

            foreach (var item in list)
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.EmpNum = item.EmpNum;
                emp.email = item.email;
                emp.fullname = item.firstName + " " + item.lastName;
                emp.gender = item.gender;
                emp.UserRole = item.UserRole;
                emp.deptName = item.deptName;
                emp.typeName = item.typeName;
                emp.Picture = item.Picture;
                vm.Add(emp);
            }
            return View(vm.ToList());
        }

        //========================Contact Paster==================================
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(ContactUsViewModel model, IEnumerable<HttpPostedFileBase> files)
        {
            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (ModelState.IsValid && Request.IsAuthenticated)
            {
                ContactUs contactus = new ContactUs();
                contactus.User = usermanager.FindByEmail(User.Identity.Name);
                var contact = new ContactUs
                {
                    subject = model.subject,
                    body = model.body,
                    userName = contactus.User.fullName,
                    read = false,
                    datesent = DateTime.Now
                    //category = model.category
                };
                db.contactUs.Add(contact);

                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var screen = new Screenshot
                        {
                            ImageMimeType = file.ContentType,
                            filename = Path.GetFileName(file.FileName),
                            ImageData = ConvertToBytes(file)
                        };
                        db.screenshots.Add(screen);
                    }
                    else
                    {
                        //DO NOTHING -WAITING INSTRUCTIONS-
                    }
                }
                //var category = _context.Assets.Where(x => x.employeeNumber.Equals(User.Identity.Name)).Select(x => x.assetNumber);
                //ViewBag.Category = category;
                db.SaveChanges();
                TempData["Success"] = "Message was sent successfully.";
            }
            ModelState.Clear();
            return View();
        }
        public byte[] ConvertToBytes(HttpPostedFileBase Image)
        {
            byte[] ImageBytes = null;
            BinaryReader reader = new BinaryReader(Image.InputStream);
            ImageBytes = reader.ReadBytes((int)Image.ContentLength);
            return ImageBytes;
        }
    }
}