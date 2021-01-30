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
    public class CalendarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Employee individual roster
        public ActionResult CalanderViewModelAction(int? page)
        {
            List<CalanderViewModel> vm = new List<CalanderViewModel>();
            var list = (from e in db.employees
                        join r in db.rosters on e.EmpNum equals r.EmpNum
                        select new
                        {
                            e.email,
                            e.EmpNum,
                            e.firstName,
                            e.lastName,
                            r.Date,
                            r.startTime,
                            r.endTime
                        }).Where(x => x.email.Equals(User.Identity.Name))
                         .OrderBy(m => m.Date).ToList();

            foreach (var item in list)
            {
                CalanderViewModel emp = new CalanderViewModel();
                emp.id = item.EmpNum;
                emp.email = item.email;
                emp.text = item.firstName + " " + item.lastName;
                emp.Date = item.Date;
                //emp.Date = item.Date;
                //emp.Date = DateTime.ParseExact(item.Date.ToString(), "dd/MM/yyyy", null);
                emp.startTime = item.startTime;
                emp.endTime = item.endTime;
                //emp.start_date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day, item.startTime.Hours, item.startTime.Minutes, item.startTime.Seconds);
                //emp.end_date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day, item.endTime.Hours, item.endTime.Minutes, item.endTime.Seconds);
                vm.Add(emp);
            }
            int PageSize = 6;
            int PageNumber = (page ?? 1);
            return View(vm.ToPagedList(PageNumber, PageSize));
        }
    }
}
