using Golden.Fish.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golden.Fish.Desktop.Models
{
    class EventListHandler
    {
        public EventListHandler()
        {
            Items = new List<Event>();
        }

        public List<Event> Items { get; private set; }

        public void Add(Event item)
        {
            Items.Add(item);
        }
    }
}
