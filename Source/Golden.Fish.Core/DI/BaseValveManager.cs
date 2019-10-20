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
        
        public void SetupValve(Valve valve, bool isValueHigh = false)
        {
            valve.PinValue = isValueHigh;
            mGpioController.SetPinMode(valve.PinNumber, PinMode.Output);
            mGpioController.Write(valve.PinNumber, valve.PinValue);
            mValves[valve] = valve.PinNumber;
        }

        public void ToggleValve(Valve valve)
        {
            if (!mValves.ContainsKey(valve))
            {
                throw new Exception("Valve was not setup");
            }
            // TODO: Cleanup dictionary. pin number is part of the Valve object
            int pinNum = mValves[valve];
            bool currentValue = (bool)mGpioController.Read(pinNum);
            var newValue = !currentValue;
            mGpioController.Write(pinNum, newValue);
        }

        public void SetValveValue(Valve valve, bool newValue)
        {
            bool currentValue = (bool)mGpioController.Read(valve.PinNumber);
            if (currentValue != newValue)
            {
                ToggleValve(valve);
            }
        }
    }
}
