using System;
using System.Collections.Generic;
using System.Text;

namespace Golden.Fish.Core.Services
{
    public interface ICronScheduler
    {
        public void Start();

        public void Stop();

        public int AddJob<Tin>(string cronTime, Action<Tin> func, Tin args);

        public bool DeleteJob(int jobId);
    }
}
