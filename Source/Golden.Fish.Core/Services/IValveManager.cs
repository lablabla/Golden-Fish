using Golden.Fish.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Device.Gpio;

namespace Golden.Fish.Core.Services
{
    public interface IValveManager
    {
        public void SetupValve(Valve valve, bool isValueHigh = false);
        public void ToggleValve(Valve valve);
        public void SetValveValue(Valve valve, bool newValue);
    }
}
