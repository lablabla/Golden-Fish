using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Text;

namespace Golden.Fish.Core.Models
{
    [Serializable]
    [JsonObject]
    public class Valve
    {
        public string Name { get; set; }
        public int PinNumber { get; set; }
        public bool PinValue { get; set; }
    }
}
