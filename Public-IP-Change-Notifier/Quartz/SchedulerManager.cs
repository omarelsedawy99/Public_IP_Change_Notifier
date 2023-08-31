using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_IP_Change_Notifier.Quartz
{
    class SchedulerManager
    {
        private IScheduler ObjScheduler { get; set; }
        public SchedulerManager()
        {
            ObjScheduler = StdSchedulerFactory.GetDefaultScheduler()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            Start();
        }
        public IJobDetail GetJobs()
        {
            IJobDetail job = JobBuilder.Create<LogicJob>().Build();
            return job;
        }
        public ITrigger GetTriggerSimple(IJobDetail jobDetailObject, int secondTime = 3)
        {
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(jobDetailObject.Key.Name, jobDetailObject.Key.Group + jobDetailObject.Key.Name)
                .StartNow()
                .ForJob(jobDetailObject.Key)
                .WithSimpleSchedule(s => s
                .WithIntervalInSeconds(3)
                .RepeatForever())
                .Build();
            ObjScheduler.ScheduleJob(jobDetailObject, trigger);
            return trigger;
        }
        public ITrigger GetTriggerCron(IJobDetail jobDetailObject, string cronExpression = "* * * * * ? *")
        {
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(jobDetailObject.Key.Name, jobDetailObject.Key.Group + jobDetailObject.Key.Name)
                .WithCronSchedule(cronExpression)
                .StartNow()
                .ForJob(jobDetailObject.Key)
                .Build();
            ObjScheduler.ScheduleJob(jobDetailObject, trigger);
            return trigger;
        }
        public void Start()
        {
            ObjScheduler.Start();
        }
        public void Stop()
        {
            ObjScheduler.Shutdown();
        }
    }
}
