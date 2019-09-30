using Golden.Fish.Core.Services;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Golden.Fish.Core
{
    public class BaseCronScheduler : ICronScheduler
    {
        #region Private Members

        private IScheduler mQuartzzScheduler;
        private Dictionary<int, Tuple<JobKey, TriggerKey>> mKeys = new Dictionary<int, Tuple<JobKey, TriggerKey>>();
        private int mNextId = 0;

        private readonly object mLock = new object();

        #endregion

        #region Constructor

        public BaseCronScheduler()
        {
            ConfigureQuartzSchedulerAsync();
            Start();
        }

        #endregion


        #region Interface Implementation

        public int AddJob<Tin>(string cronTime, Action<Tin> func, Tin args)
        {
            JobDataMap jobDataMap = new JobDataMap
            {
                { "func", func },
                { "args", args }
            };
            IJobDetail job = JobBuilder.Create<JobWrapper<Tin>>()  
                .UsingJobData(jobDataMap)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithCronSchedule(cronTime)
                .Build();

            mQuartzzScheduler.ScheduleJob(job, trigger);
            int id = -1;
            lock (mLock)
            {
                id = mNextId;
                mNextId++;
                Tuple<JobKey, TriggerKey> keys = new Tuple<JobKey, TriggerKey>(job.Key, trigger.Key);
                mKeys.Add(id, keys);
            }
            return id;
        }

        public bool DeleteJob(int jobId)
        {
            lock (mLock)
            {
                if (!mKeys.ContainsKey(jobId))
                {
                    return false;
                }
                return mKeys.Remove(jobId);
            }
        }

        public async void Start()
        {
            await mQuartzzScheduler.Start();
        }

        public async void Stop()
        {
            await mQuartzzScheduler.Shutdown();
        }

        #endregion

        #region Private Helper Functions

        private async void ConfigureQuartzSchedulerAsync()
        {
            mQuartzzScheduler = await new StdSchedulerFactory().GetScheduler();
        }

        #endregion

        #region Private Helper Classes

        public class JobWrapper<Tin> : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                var map = context.JobDetail.JobDataMap;
                Action<Tin> func = (Action<Tin>)map["func"];
                Tin args = (Tin)map["args"];
                func(args);
                return Task.Delay(0);
            }
        }

        #endregion
    }
}
