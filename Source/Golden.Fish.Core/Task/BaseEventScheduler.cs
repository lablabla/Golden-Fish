using Dna;
using Golden.Fish.Core.Models;
using Golden.Fish.Core.Services;
using System;
using System.Collections.Generic;
using static Dna.FrameworkDI;

namespace Golden.Fish.Core
{
    public class BaseEventScheduler : IEventScheduler
    {
        private ICronScheduler mCronScheduler;
        private IValveManager mValveManager;

        private Dictionary<Event, int> mEventsIds;

        public BaseEventScheduler(ICronScheduler cronScheduler, IValveManager valveManager)
        {
            mCronScheduler = cronScheduler;
            mValveManager = valveManager;

            mEventsIds = new Dictionary<Event, int>();
        }
        public void Start()
        {
            mEventsIds = new Dictionary<Event, int>();
            mCronScheduler.Start();
        }

        public void Stop()
        {
            mCronScheduler.Stop();
        }

        public int AddEvent(Event @event)
        {
            int id = mCronScheduler.AddJob(@event.CronTime, EventFunc, @event);
            mEventsIds[@event] = id;
            return id;
        }

        public bool DeleteJob(Event @event)
        {
            if (!mEventsIds.ContainsKey(@event))
            {
                // Job doesn't exist. can't delete
                return false;
            }
            return mCronScheduler.DeleteJob(mEventsIds[@event]);
        }

        private void EventFunc(Event @event)
        {

            Logger.LogDebugSource($"Fired function for event id {@event.Id}, Setting valve {@event.Valve.Name} to: {(@event.NextValue ? "On" : "Off")}");

            // Use IValveManager to toggle valve
            mValveManager.SetValveValue(@event.Valve, @event.NextValue);

        }
    }
}
