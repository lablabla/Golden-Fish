using Golden.Fish.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Golden.Fish.Core.Services
{
    public interface IClientDataStore
    {

        /// <summary>
        /// Makes sure the client data store is correctly set up
        /// </summary>
        /// <returns>Returns a task that will finish once setup is complete</returns>
        Task EnsureDataStoreAsync();

        Task<IReadOnlyCollection<Valve>> GetValvesAsync();
        
        Task SetValvesAsync(ICollection<Valve> valves);

        Task<IReadOnlyCollection<Event>> GetScheduledJobsAsync();

        Task SetScheduledJobsAsync(ICollection<Event> jobs);

        event EventHandler JobsChanged;

        event EventHandler ValvesChanged;
    }
}
