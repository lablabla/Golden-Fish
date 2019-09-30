using Golden.Fish.Core.Models;
using Golden.Fish.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Device.Gpio;

namespace Golden.Fish.Core
{
    public class BaseValveManager : IValveManager
    {
        private GpioController mGpioController = new GpioController();

        private Dictionary<Valve, int> mValves = new Dictionary<Valve, int>();
        
        public void SetupValve(Valve valve, int pinNum, bool isValueHigh = false)
        {
            valve.PinValue = isValueHigh;
            mGpioController.SetPinMode(pinNum, PinMode.Output);
            mGpioController.Write(pinNum, valve.PinValue);
            mValves[valve] = pinNum;
        }

        public void ToggleValve(Valve valve)
        {
            if (!mValves.ContainsKey(valve))
            {
                throw new Exception("Valve was not setup");
            }
            int pinNum = mValves[valve];
            bool currentValue = (bool)mGpioController.Read(pinNum);
            var newValue = !currentValue;
            mGpioController.Write(pinNum, newValue);
        }
    }
}
