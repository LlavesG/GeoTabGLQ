using Geotab.Checkmate.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoTabGLQ.Helper
{
    public static  class DevicePropertyHellper
    {
        public static string? getVIN(Device device)
        {
            if (device is XDevice xDevice)
            {
                return xDevice.EngineVehicleIdentificationNumber;
            }
            return null;
        }
        
    }
}
