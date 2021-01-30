using PagedList;
using PowerOfGod.Domain.Context;
using PowerOfGod.ViewModel.EmployeeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerOfGod.Web.Controllers
{
    public class LeaveModelViewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult LeaveViewModelView(int? page)
        {
            List<LeaveViewModel> vm = new List<LeaveViewModel>();
            var list = (from el in db.employeeLeavels
                        join l in db.LeaveTypes
                        on el.leaveTypeID equals l.leaveTypeID
                        select new
                        {
                            el.LeaveID,
                            l.type,
                            el.reason,
                            el.startDate,
                            el.endDate,
                            el.numberOfDay,
                            el.status,
                            el.User.Email,
                            el.createdBy,
                            el.updateBy
                        }).Where(x => x.Email.Equals(User.Identity.Name)).ToList();

            foreach (var item in list)
            {
                LeaveViewModel lvm = new LeaveViewModel();
                lvm.LeaveID = item.LeaveID;
                lvm.type = item.type;
                lvm.reason = item.reason;
                lvm.startDate = item.startDate;
                lvm.endDate = item.endDate;
                lvm.numberOfDay = item.numberOfDay;
                lvm.status = item.status;
                lvm.email = item.Email;
                lvm.createdBy = item.createdBy;
                lvm.updateBy = item.updateBy;
                vm.Add(lvm);
            }
            var query = vm.ToList();
            int PageSize = 6;
            int PageNumber = (page ?? 1);
            return View(query.ToPagedList(PageNumber, PageSize));
        }
    }
}
