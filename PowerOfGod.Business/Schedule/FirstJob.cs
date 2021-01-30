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
    class FirstJob : IJob
    {
        //=================Assigning Permanent(Cleaners)=================
        public void Execute(IJobExecutionContext context)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<Roster> rst = new List<Roster>();
            List<DateTime> dates = new List<DateTime>();
            List<int> q = new List<int>();

            int year = 2018;
            int month = 0;
            var min = 0;
            int count = 1;
            //var count = 0;
            DayOfWeek day = DayOfWeek.Sunday;

            TimeSpan timeS = new TimeSpan(0, 8, 0, 0, 0);
            TimeSpan timeE = new TimeSpan(0, 15, 0, 0, 0);

            for (month = 8; month <= 12; month ++)
            {
                //Get sundays within the month
                System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                for (int i = 1; i <= currentCulture.Calendar.GetDaysInMonth(year, month); i++)
                {
                    DateTime d = new DateTime(year, month, i);
                    if (d.DayOfWeek == day)
                    {
                        //store date on the queue
                        dates.Add(d);
                    }
                }
            }

            //looping through the list
            foreach (var emp in db.employees)
            {
                if (emp.typeCode == "V" && emp.deptCode == 1)
                {
                    //store item on the queue
                    q.Add(emp.queue);
                }
            }

            //min in the queue
            //min = q.Min();
            for (int i = 0; i < 4; i++)
            {
                //assigning employees to the roster
                foreach (var emp in db.employees)
                {
                    //break the loop
                    if (emp.queue == 16)
                    {
                        break;
                    }

                    Roster r = new Roster();
                    if (count != 6)
                    {
                        if (emp.status == "Available" && emp.deptCode == 1 && emp.typeCode == "V" && emp.queue == min)
                        {
                            r.Date = dates[min - 1];
                            r.startTime = timeS;
                            r.endTime = timeE;
                            r.EmpNum = emp.EmpNum;
                            r.deptCode = 1;
                            emp.queue += 2;
                            emp.status = "Not Available";
                            //if(r.Date)
                            rst.Add(r);
                            db.rosters.Add(r);
                            count++;
                        }
                    }
                    //min++;
                }//foreach loop end
                count = 0;
            }//for loop end    
            db.SaveChanges();
            Debug.WriteLine("roster added at " + DateTime.Now.ToString());
        }
    }
}
