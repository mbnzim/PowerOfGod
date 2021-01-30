using PowerOfGod.Domain.Context;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Business.Schedule
{
    //=================Assigning Volunteer(Cleaners)=================
    class SecondJob : IJob
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Execute(IJobExecutionContext context)
        {
            var min = 0;
            List<int> q = new List<int>();

            //looping through the employee table
            foreach (var emp in db.employees)
            {
                if (emp.typeCode == "V" && emp.deptCode == 1)
                {
                    //store item in the queue
                    q.Add(emp.queue);
                }
            }
            //min value
           // min = q.Min();
            foreach (var item in db.employees)
            {
                if (item.queue == min)
                {
                    //change the status when queue equal min
                    item.status = "Available";
                }
            }
            db.SaveChanges();
            Debug.WriteLine("Changing the status of the employee at " + DateTime.Now.ToString());
        }
    }
}
