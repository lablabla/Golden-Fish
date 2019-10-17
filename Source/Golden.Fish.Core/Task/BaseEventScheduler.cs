using Dna;
using Golden.Fish.Core.Models;
using Golden.Fish.Core.Services;
using System;
using static Dna.FrameworkDI;
using static Golden.Fish.Core.CoreDI;

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
            return CronScheduler.AddJob(@event.CronTime, EventFunc, @event);
        }

        public bool DeleteJob(Event @event)
        {
            throw new NotImplementedException();
        }

        private void EventFunc(Event @event)
        {

            Logger.LogDebugSource($"Fired function for event id {@event.Id}");
            // Todo:
            // Use IValveManager to toggle valve
        }
    }
}
