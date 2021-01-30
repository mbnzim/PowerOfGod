using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Business.Schedule
{
    public class TestJobScheduler
    {
        public static void Start()
        {
            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);
            //Schedule
            HolidayCalendar cal = new HolidayCalendar();
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //====================Voluntee Cleaner============================

            //Assign employees on the roster
            IJobDetail firstJob = JobBuilder.Create<FirstJob>()
                                     .WithIdentity("firstJob")
                                     .Build();
            //First Trigger
            ITrigger firstTrigger = TriggerBuilder.Create()
                                             .WithIdentity("firstTrigger")
                                             .StartAt(runTime)
                                             .WithCronSchedule("0/6 * * * * ?")
                                             .Build();

            //Change the employees status and queue number
            IJobDetail secondJob = JobBuilder.Create<SecondJob>()
                                     .WithIdentity("secondJob")
                                     .Build();
            //Second Trigger
            ITrigger secondTrigger = TriggerBuilder.Create()
                                             .WithIdentity("secondTrigger")
                                             .StartAt(runTime)
                                             .WithCronSchedule("0/5 * * * * ?")
                                             .Build();

            //====================Permanent Cleaner============================

            //Assign employees on the roster
            IJobDetail pcleanerJob = JobBuilder.Create<PCleanerJobFirst>()
                                     .WithIdentity("pcleanerJob")
                                     .Build();
            //Trigger job
            ITrigger pcleanerTrigger = TriggerBuilder.Create()
                                             .WithIdentity("pcleanerTrigger")
                                             .StartAt(runTime)
                                             .WithCronSchedule("0/6 * * * * ?")
                                             .Build();

            //Assign employees on the roster
            /*IJobDetail pcleanerJobSecond = JobBuilder.Create<PCleanerJobFirst>()
                                            .WithIdentity("pcleanerJobSecond")
                                            .Build();
            //Trigger job
            ITrigger pcleanerTriggerSecond = TriggerBuilder.Create()
                                             .WithIdentity("pcleanerTriggerSecond")
                                             .StartAt(runTime)
                                             .WithCronSchedule("0 0 12 1 * ?")
                                             .Build();*/

            //========================PERMANANT EMPLOYEES===============================

            //Cleaner Job
            IJobDetail CleanerJob = JobBuilder.Create<PClearJob>()
                                     .WithIdentity("CleanerJob")
                                     .Build();

            //Cleaner Trigger
            ITrigger CleanerTrigger = TriggerBuilder.Create()
                                          .WithIdentity("CleanerTrigger")
                                          .StartAt(runTime)
                                          .WithCronSchedule("0 1 * * * ?")
                                          .Build();
            //========================/PERMANANT EMPLOYEES===============================


            //Executing cleaners jobs
            scheduler.ScheduleJob(firstJob, firstTrigger);
            scheduler.ScheduleJob(secondJob, secondTrigger);

            //scheduler.ScheduleJob(pcleanerJob, pcleanerTrigger);
            //scheduler.ScheduleJob(CleanerJob, CleanerTrigger);
            //scheduler.ScheduleJob(pcleanerJobSecond, pcleanerTriggerSecond);

            //Executing Security dayshift jobs
            //scheduler.ScheduleJob(securityAssignJob, securityAssignTrigger);
            //scheduler.ScheduleJob(securityAssignJob2, securityAssignTrigger2);
        }
    }
}
