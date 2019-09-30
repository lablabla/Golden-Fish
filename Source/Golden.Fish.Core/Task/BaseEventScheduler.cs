using Golden.Fish.Core.Models;
using Golden.Fish.Core.Services;
using static Golden.Fish.Core.CoreDI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golden.Fish.Core
{
    public class BaseEventScheduler : IEventScheduler
    {

        public void Start()
        {
            CronScheduler.Start();
        }

        public void Stop()
        {
            CronScheduler.Stop();
        }

        public int AddEvent(Event @event)
        {
            return CronScheduler.AddJob<Event>(@event.CronTime, EventFunc, @event);
        }

        public bool DeleteJob(Event @event)
        {
            throw new NotImplementedException();
        }

        private void EventFunc(Event @event)
        {
            // Todo:
            // Use IValveManager to toggle valve
        }
    }
}
