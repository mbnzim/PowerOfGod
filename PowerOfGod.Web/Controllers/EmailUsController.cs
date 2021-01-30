using PagedList;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.UserContact;
using PowerOfGod.ViewModel.EmployeeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PowerOfGod.Web.Controllers
{
    public class EmailUsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //=============Email begging===================
        //Inbox
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inbox(string search)
        {
            if (search != "")
            {
                var query = (from a in db.contactUs.ToList()
                             select new ContactUsViewModel
                             {
                                 id = a.contactId,
                                 read = a.read,
                                 subject = a.subject,
                                 body = a.body,
                                 username = a.userName,
                                 datesent = a.datesent
                             }).Where(x => x.body.Contains(search) || x.subject.Contains(search));
                int count = (query.ToList().Where(x => x.read.Equals(false))).Count();
                int count2 = query.ToList().Count();
                ViewBag.Mail = count2;
                ViewBag.Inbox = count;
                return View(query);
            }
            else
            {
                return RedirectToAction("AllMessages");
            }

            //var inbox = _context.Contactus.ToList();
            //return View(inbox);
        }
        //AllMessages
        public ActionResult AllMessages()
        {
            ViewBag.AllMessages = db.contactUs.ToList().Count();
            ViewBag.OpenedMessages = db.contactUs.ToList().Where(x => x.read.Equals(true)).Count();
            ViewBag.ClosedMessages = db.contactUs.ToList().Where(x => x.read.Equals(false)).Count();
            return View();
        }

        public ActionResult Inbox()
        {
            var query = (from a in db.contactUs.ToList()
                         select new ContactUsViewModel
                         {
                             id = a.contactId,
                             read = a.read,
                             subject = a.subject,
                             body = a.body,
                             username = a.userName,
                             datesent = a.datesent
                         })
                         .OrderByDescending(x => x.datesent);
            //int PageSize = 6;
            //int PageNumber = (page ?? 1);
            return View(query);
            //var inbox = _context.Contactus.ToList();
            //return View(inbox);
        }
        //InboxCount
        public ActionResult InboxCount()
        {
            ViewBag.InboxNote = (db.contactUs.ToList().Where(x => x.read.Equals(false))).Count();
            return View();
        }

        public ActionResult InboxDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inbox = db.contactUs.Find(id);

            Session["empNo"] = inbox.userName.ToString();
            Session["Subject"] = inbox.subject.ToString();
            Session["Body"] = inbox.body.ToString();
            if (inbox.category != null)
            {
                Session["AssetNumber"] = inbox.category.ToString();
            }
            var screenshots = db.screenshots.Where(m => m.contactId == inbox.contactId).ToList();
            if (inbox == null)
            {
                return HttpNotFound();
            }

            var data = new Tuple<ContactUs, IEnumerable<Screenshot>>(inbox, screenshots);
            inbox.read = true;
            db.SaveChanges();
            return View(data);
        }
        //unReadmail
        public ActionResult unReadmail(int? page)
        {
            var query = db.contactUs.Where(x => x.read.Equals(false)).ToList();
            int PageSize = 4;
            int PageNumber = (page ?? 1);
            return View(query.ToPagedList(PageNumber, PageSize));
        }
        //unReadmail
        public ActionResult OpenedMail(int? page)
        {
            var query = db.contactUs.Where(x => x.read.Equals(true)).ToList();
            int PageSize = 4;
            int PageNumber = (page ?? 1);
            return View(query.ToPagedList(PageNumber, PageSize));
        }
        //====================End Email =====================
    }
}