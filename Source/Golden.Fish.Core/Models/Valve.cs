using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Text;

namespace Golden.Fish.Core.Models
{
    public class Valve
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PinNumber { get; set; }
        public PinValue PinValue { get; set; }
    }
}
