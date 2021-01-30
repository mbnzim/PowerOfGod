using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Employee;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PowerOfGod.Business.Schedule
{
    class PClearJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //===========================Monday========================
                //int j = 0;
                int count = 0;
                int year = 2018;
                int month;
                List<Roster> rst = new List<Roster>();
                List<DateTime> dates = new List<DateTime>();
                TimeSpan timeS = new TimeSpan(0, 8, 0, 0, 0);
                TimeSpan timeE = new TimeSpan(0, 15, 0, 0, 0);

                DayOfWeek day = DayOfWeek.Monday;

                //Get Monday within the month
                for (month = 1; month <= 12; month++)
                {
                    System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                    for (int i = 1; i <= currentCulture.Calendar.GetDaysInMonth(year, month); i++)
                    {
                        DateTime d = new DateTime(year, month, i);
                        if (d.DayOfWeek == day)
                        {
                            if (!(d < DateTime.Now))
                            {
                                //store date on the queue              
                                dates.Add(d);
                            }
                        }
                    }
                }

                for (int i = 1; i < dates.Count(); i++)
                {
                    foreach (var emp in db.employees)
                    {
                        //var m = emp.EmpNum;
                        Roster r = new Roster();
                        if (count == dates.Count())
                        {
                            break;
                        }
                        if (emp.status == "Available" && emp.deptCode == 1 && emp.typeCode == "P")
                        {
                            r.Date = dates[count];
                           // r.EmpNum = emp.firstName + emp.lastName;
                            r.EmpNum = emp.EmpNum;
                            r.startTime = timeS;
                            r.endTime = timeE;
                            r.deptCode = 1;
                            emp.status = "Available";
                            rst.Add(r);
                            db.rosters.Add(r);
                            //count++;
                        }
                    }
                    count++;
                }
               db.SaveChanges();
               Debug.WriteLine("roster generated at " + DateTime.Now.ToString());            
        }
    }
}
