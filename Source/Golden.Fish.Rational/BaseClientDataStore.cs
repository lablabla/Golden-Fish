using Golden.Fish.Core.Models;
using Golden.Fish.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Golden.Fish.Rational
{
    public class BaseClientDataStore : IClientDataStore
    {
        #region Protected Members

        /// <summary>
        /// The database context for the client data store
        /// </summary>
        protected ClientDataStoreDbContext mDbContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext">The database to use</param>
        public BaseClientDataStore(ClientDataStoreDbContext dbContext)
        {
            // Set local member
            mDbContext = dbContext;
        }

        #endregion

        #region Interface Implementation


        public async Task EnsureDataStoreAsync()
        {
            // Make sure the database exists and is created
            await mDbContext.Database.EnsureCreatedAsync();
        }

        public async Task<IReadOnlyCollection<Valve>> GetValvesAsync()
        {
            return await mDbContext.Valves.ToListAsync()
                .ContinueWith(x => x.Result as IReadOnlyList<Valve>);
        }
        public async Task SetValvesAsync(ICollection<Valve> valves)
        {
            // Set to new valves
            mDbContext.Valves.RemoveRange(mDbContext.Valves);

            foreach (var valve in valves)
            {
                mDbContext.Valves.Add(valve);
            }

            // Save changes
            await mDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Event>> GetScheduledJobsAsync()
        {
            return await mDbContext.ScheduledJobs.ToListAsync()
                .ContinueWith(x => x.Result as IReadOnlyList<Event>);
        }

        public async Task SetScheduledJobsAsync(ICollection<Event> jobs)
        {
            // Set to new valves
            mDbContext.ScheduledJobs.RemoveRange(mDbContext.ScheduledJobs);

            foreach (var job in jobs)
            {
                mDbContext.ScheduledJobs.Add(job);
            }

            // Save changes
            await mDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
