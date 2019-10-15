using Golden.Fish.Core.Models;
using Golden.Fish.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Golden.Fish.Rational
{
    public class ClientDataStoreDbContext : DbContext
    {
        #region DbSets 

        public DbSet<Valve> Valves { get; set; }
        public DbSet<Event> ScheduledJobs { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ClientDataStoreDbContext(DbContextOptions<ClientDataStoreDbContext> options) : base(options) { }

        #endregion

        #region Model Creating

        /// <summary>
        /// Configures the database structure and relationships
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API

            // Configure Events
            // --------------------------
            //
            // Set Id as primary key
            modelBuilder.Entity<Event>().HasKey(a => a.Id);
            modelBuilder.Entity<Event>().HasOne(v => v.Valve);

        }

        #endregion
    }
}
