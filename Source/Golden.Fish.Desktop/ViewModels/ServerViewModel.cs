using Dna;
using Golden.Fish.Core;
using Golden.Fish.Core.Models;
using Golden.Fish.Core.Services;
using Golden.Fish.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dna.FrameworkDI;
using static Golden.Fish.Desktop.DI;

namespace Golden.Fish.Desktop
{
    public class ServerViewModel : BaseViewModel, IDisposable
    {
        private readonly EventListHandler mItemHandler;

        private readonly IValveManager mValveManager;

        public List<Event> Items => mItemHandler.Items;

        #region Constructor

        public ServerViewModel(IValveManager valveManager)
        {
            mValveManager = valveManager;
            mItemHandler = new EventListHandler();
            Initialize();
        }

        #endregion

        private async void Initialize()
        {
            await SetupJobsScheduling().ConfigureAwait(false);

            // Setup Valves
            await SetupValves();

            // Add listeners
            ClientDataStore.JobsChanged += ClientDataStore_JobsChanged;
            ClientDataStore.ValvesChanged += ClientDataStore_ValvesChanged;
        }

        private async Task SetupJobsScheduling()
        {
            CoreDI.EventScheduler.Start();

            List<Event> list = (List<Event>)await ClientDataStore.GetScheduledJobsAsync();
            foreach (var @event in list)
            {
                // Add to list
                mItemHandler.Add(@event);

                // Initialize the event
                CoreDI.EventScheduler.AddEvent(@event);
            }
        }

        private async Task SetupValves()
        {
            List<Valve> list = (List<Valve>)await ClientDataStore.GetValvesAsync();
            foreach(var valve in list)
            {
                // Initialize valve
                mValveManager.SetupValve(valve, false);
            }
        }

        private void ClientDataStore_ValvesChanged(object sender, EventArgs e)
        {
            Logger.LogDebugSource("ClientDataStore_ValvesChanged Called...");
        }

        private void ClientDataStore_JobsChanged(object sender, EventArgs e)
        {
            Logger.LogDebugSource("ClientDataStore_JobsChanged Called...");
        }

        public void Dispose()
        {
            CoreDI.EventScheduler.Stop();
        }
    }
}
