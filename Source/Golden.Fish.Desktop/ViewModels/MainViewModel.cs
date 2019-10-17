using Golden.Fish.Core;
using Golden.Fish.Core.Models;
using Golden.Fish.Desktop.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using static Golden.Fish.Desktop.DI;
namespace Golden.Fish.Desktop
{
    public class MainViewModel : BaseViewModel
    {
        private readonly EventListHandler _itemHandler;

        public List<Event> Items => _itemHandler.Items;

        #region Constructor

        public MainViewModel()
        {
            _itemHandler = new EventListHandler();
            Initialize();
        }

        private async void Initialize()
        {
            CoreDI.EventScheduler.Start();

            List<Event> list = (List<Event>)await ClientDataStore.GetScheduledJobsAsync();
            foreach (var @event in list)
            {
                // Add to list
                _itemHandler.Add(@event);

                // Initialize the event
                CoreDI.EventScheduler.AddEvent(@event);
            }
        }


        private async void SaveEvents(List<Event> list)
        {
            await ClientDataStore.SetScheduledJobsAsync(list);

        }

        #endregion
    }
}
