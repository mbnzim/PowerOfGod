using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PowerOfGod.Business.Logic
{
    public class Logic
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //**************************CLEANER*************************
        //=====================Monday=====================
        public IEnumerable<Roster> AddEmployeeMonday()
        {
            //int j = 0;
            int count = 0;
            int year = 2020;
            int month;
            List<DateTime> dates = new List<DateTime>();
            List<Roster> rst = new List<Roster>();
            TimeSpan timeS = new TimeSpan(0, 8, 0, 0, 0);
            TimeSpan timeE = new TimeSpan(0, 15, 0, 0, 0);
            DayOfWeek day = DayOfWeek.Monday;

            //Get Monday within the month
            for (month = 1; month <= 11; month++)
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
                       // r.Shift = "Day";
                        r.Date = dates[count];
                        // r.EmpNum = emp.firstName + emp.lastName;
                        r.EmpNum = emp.EmpNum;
                        r.startTime = timeS;
                        r.endTime = timeE;
                        r.deptCode = 1;
                       
                        rst.Add(r);
                        db.rosters.Add(r);
                    }
                    //emp.status = "Not Available";
                }
                count++;
            }
            db.SaveChanges();
            return db.rosters.ToList();
        }
        //=====================Wednesday=====================
         public IEnumerable<Roster> AddEmployeeWednesday()
         {
             //int j = 0;
             int count = 0;
             int year = 2020;
             int month;
             List<Roster> rst = new List<Roster>();
             List<DateTime> dates = new List<DateTime>();
             TimeSpan timeS = new TimeSpan(0, 9, 0, 0, 0);
             TimeSpan timeE = new TimeSpan(0, 16, 0, 0, 0);
             DayOfWeek day = DayOfWeek.Wednesday;

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
                       // r.Shift = "Day";
                         r.Date = dates[count];
                         // r.EmpNum = emp.firstName + emp.lastName;
                         r.EmpNum = emp.EmpNum;
                         r.startTime = timeS;
                         r.endTime = timeE;
                         r.deptCode = 1;

                         rst.Add(r);
                         db.rosters.Add(r);
                     }
                     //emp.status = "Not Available";
                 }
                 count++;
             }
             db.SaveChanges();
             return db.rosters.ToList();
         }
         //=====================Friday=====================
         public IEnumerable<Roster> AddEmployeeFriday()
         {
             //int j = 0;
             int count = 0;
             int year = 2020;
             int month;
             List<Roster> rst = new List<Roster>();
             List<DateTime> dates = new List<DateTime>();
             TimeSpan timeS = new TimeSpan(0, 12, 0, 0, 0);
             TimeSpan timeE = new TimeSpan(0, 15, 0, 0, 0);
             DayOfWeek day = DayOfWeek.Friday;

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
                         //r.Shift = "Day";
                         r.Date = dates[count];
                         // r.EmpNum = emp.firstName + emp.lastName;
                         r.EmpNum = emp.EmpNum;
                         r.startTime = timeS;
                         r.endTime = timeE;
                         r.deptCode = 1;

                         rst.Add(r);
                         db.rosters.Add(r);
                     }
                     //emp.status = "Not Available";
                 }
                 count++;
             }
             db.SaveChanges();
             return db.rosters.ToList();
         }
        //**************************/CLEANER*************************


        //**************************SECURITY*************************
        //=====================Monday Shift========================

        public IEnumerable<Roster> SecurityMondayShift()
        {
            //int j = 0;
            int count = 0;
            int year = 2020;
            int month;
            List<DateTime> dates = new List<DateTime>();
            List<Roster> rst = new List<Roster>();
            TimeSpan timeS;
            TimeSpan timeE;

            DayOfWeek day = DayOfWeek.Monday;

            //Get Monday within the month
            for (month = 1; month <= 11; month++)
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
                    //Day Shift
                    if (emp.status == "Available" && emp.deptCode == 2 && emp.typeCode == "P" && emp.shift == "Day")
                    {
                        timeS = new TimeSpan(0, 6, 0, 0, 0);
                        timeE = new TimeSpan(0, 18, 0, 0, 0);
                        //r.Shift = "Day";
                        r.Date = dates[count];
                        // r.EmpNum = emp.firstName + emp.lastName;
                        r.EmpNum = emp.EmpNum;
                        r.startTime = timeS;
                        r.endTime = timeE;
                        r.deptCode = 2;
                        emp.shift = "Day";
                        rst.Add(r);
                        db.rosters.Add(r);
                    //Night shift
                    }else if (emp.status == "Available" && emp.deptCode == 2 && emp.typeCode == "P" && emp.shift == "Night")
                    {
                        timeS = new TimeSpan(0, 18, 0, 0, 0);
                        timeE = new TimeSpan(0, 6, 0, 0, 0);
                        //r.Shift = "Night";
                        r.Date = dates[count];
                        // r.EmpNum = emp.firstName + emp.lastName;
                        r.EmpNum = emp.EmpNum;
                        r.startTime = timeS;
                        r.endTime = timeE;
                        emp.shift = "Night";
                        r.deptCode = 2;
                        rst.Add(r);
                        db.rosters.Add(r);
                    }
                  //emp.shift = "Day";
                }
                count++;
            }
            db.SaveChanges();
            return db.rosters.ToList();
        }

        //=====================Tuesday Shift========================
        public IEnumerable<Roster> SecurityTuesdayShift()
        {
            //int j = 0;
            int count = 0;
            int year = 2018;
            int month;
            List<DateTime> dates = new List<DateTime>();
            List<Roster> rst = new List<Roster>();
            TimeSpan timeS;
            TimeSpan timeE;

            DayOfWeek day = DayOfWeek.Tuesday;

            //Get Monday within the month
            for (month = 1; month <= 11; month++)
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
                    //Day Shift
                    if (emp.status == "Available" && emp.deptCode == 2 && emp.typeCode == "P" && emp.shift == "Day")
                    {
                        timeS = new TimeSpan(0, 6, 0, 0, 0);
                        timeE = new TimeSpan(0, 18, 0, 0, 0);
                        //r.Shift = "Day";
                        r.Date = dates[count];
                        // r.EmpNum = emp.firstName + emp.lastName;
                        r.EmpNum = emp.EmpNum;
                        r.startTime = timeS;
                        r.endTime = timeE;
                        r.deptCode = 2;
                        emp.shift = "Day";
                        rst.Add(r);
                        db.rosters.Add(r);
                    }
                    //Night Shift
                    else if (emp.status == "Available" && emp.deptCode == 2 && emp.typeCode == "P" && emp.shift == "Night")
                    {
                        timeS = new TimeSpan(0, 18, 0, 0, 0);
                        timeE = new TimeSpan(0, 6, 0, 0, 0);
                        //r.Shift = "Night";
                        r.Date = dates[count];
                        // r.EmpNum = emp.firstName + emp.lastName;
                        r.EmpNum = emp.EmpNum;
                        r.startTime = timeS;
                        r.endTime = timeE;
                        emp.shift = "Night";
                        r.deptCode = 2;
                        rst.Add(r);
                        db.rosters.Add(r);
                    }
                    //emp.shift = "Day";
                }
                count++;
            }
            db.SaveChanges();
            return db.rosters.ToList();
        }

        //=====================Wednesday Shift========================
        public IEnumerable<Roster> SecurityWednesdayShift()
        {
            //int j = 0;
            int count = 0;
            int year = 2020;
            int month;
            List<DateTime> dates = new List<DateTime>();
            List<Roster> rst = new List<Roster>();
            TimeSpan timeS;
            TimeSpan timeE;

            DayOfWeek day = DayOfWeek.Wednesday;

            //Get Monday within the month
            for (month = 1; month <= 11; month++)
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
                    //Day Shift
                    if (emp.status == "Available" && emp.deptCode == 2 && emp.typeCode == "P" && emp.shift == "Day")
                    {
                        timeS = new TimeSpan(0, 6, 0, 0, 0);
                        timeE = new TimeSpan(0, 18, 0, 0, 0);
                        //r.Shift = "Day";
                        r.Date = dates[count];
                        // r.EmpNum = emp.firstName + emp.lastName;
                        r.EmpNum = emp.EmpNum;
                        r.startTime = timeS;
                        r.endTime = timeE;
                        r.deptCode = 2;
                        emp.shift = "Night";
                        rst.Add(r);
                        db.rosters.Add(r);
                    }
                    //Night Shift
                    else if (emp.status == "Available" && emp.deptCode == 2 && emp.typeCode == "P" && emp.shift == "Night")
                    {
                        timeS = new TimeSpan(0, 18, 0, 0, 0);
                        timeE = new TimeSpan(0, 6, 0, 0, 0);
                        //r.Shift = "Night";
                        r.Date = dates[count];
                        // r.EmpNum = emp.firstName + emp.lastName;
                        r.EmpNum = emp.EmpNum;
                        r.startTime = timeS;
                        r.endTime = timeE;
                        emp.shift = "Day";
                        r.deptCode = 2;
                        rst.Add(r);
                        db.rosters.Add(r);
                    }
                    //emp.shift = "Day";
                }
                count++;
            }
            db.SaveChanges();
            return db.rosters.ToList();
        }
        //*************************/SECURITY*************************
    }
}
