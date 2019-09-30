using Golden.Fish.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golden.Fish.Core.Services
{
    public interface IEventScheduler
    {
        public void Start();

        public void Stop();

        public int AddEvent(Event @event);

        public bool DeleteJob(Event @event);
    }
}
