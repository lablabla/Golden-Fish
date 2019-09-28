using System;
using System.Collections.Generic;
using System.Text;

namespace Golden.Fish.Core.Models
{
    public class Event
    {
        public int Id { get; set; }
        public Valve Valve { get; set; }
        public string CronTime { get; set; }
    }
}
