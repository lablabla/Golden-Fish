using Golden.Fish.Core.Models;
using Golden.Fish.Desktop.Models;
using System.Collections.Generic;

namespace Golden.Fish.Desktop
{
    public class MainViewModel : BaseViewModel
    {
        private readonly EventListHandler _itemHandler;

        public List<Event> Items
        {
            get { return _itemHandler.Items; }
        }

        #region Constructor

        public MainViewModel()
        {
            var enabled = new Event
            {
                Enabled = true,
                CronTime = "* * * * *"
            };
            var disabled = new Event()
            {
                Enabled = false,
                CronTime = "0-10 11 * * *"
            };
            _itemHandler = new EventListHandler();
            _itemHandler.Add(enabled);
            _itemHandler.Add(disabled);
            _itemHandler.Add(enabled);

        }

        #endregion
    }
}
