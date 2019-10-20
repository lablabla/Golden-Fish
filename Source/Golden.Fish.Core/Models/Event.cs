using System;
using System.Collections.Generic;
using System.Text;

namespace Golden.Fish.Core.Models
{
    [Serializable]
    public class Event
    {
        public int Id { get; set; }
        public Valve Valve { get; set; }
        public string CronTime { get; set; }
        public bool Enabled { get; set; }
        public bool NextValue { get; set; }
    }
}
