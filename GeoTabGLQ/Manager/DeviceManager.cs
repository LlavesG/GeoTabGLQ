using Geotab.Checkmate.ObjectModel;
using Geotab.Checkmate;
using GeoTabGLQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using GeoTabGLQ.Helper;
using Geotab.Checkmate.ObjectModel.Engine;

namespace GeoTabGLQ.Manager
{
    public class DeviceManager
    {
        private readonly IConfiguration configuration;
        private API api;
        public DeviceManager(IConfiguration configuration)
        {
            this.configuration = configuration;
            InitApi();
        }
        public async Task<List<DeviceDto>> SearchDevicesAsyn()
        {
            bool success = false;
            int intent = 0;
            while (!success || intent <= 10) //Control para cuando el servicio no está disponible
            {
                try
                {
                    await api.AuthenticateAsync();
                    List<DeviceDto> result = new();
                    var devices = await this.api.CallAsync<List<Device>>("Get", typeof(Device));

                    if (devices == null)
                    {
                        return null;
                    }


                    var tasks = devices.Select(async device =>
                    {

                        return await SearchDeviceStatusInfoAsyn(device);
                    });

                    var results = await Task.WhenAll(tasks);

                    return results.ToList();
                }
                catch (Geotab.Checkmate.ObjectModel.OverLimitException ex)
                {
                    Console.WriteLine("Service is temporarily unavailable. Retrying in 30 seconds...");
                    intent++;
                    InitApi();
                    await Task.Delay(TimeSpan.FromSeconds(30));
                    continue;
                }

                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                    break;
                }
            }

            return null;
        }

        private async Task<DeviceDto> SearchDeviceStatusInfoAsyn(Device device)
        {
            DeviceDto resultDevice;

            var resultsSearch = await api.CallAsync<List<DeviceStatusInfo>>("Get", typeof(DeviceStatusInfo), new
            {
                search = new
                {
                    DeviceSearch = new DeviceSearch
                    {
                        Id = device.Id
                    }
                }
            });

            if (resultsSearch.Count <= 0)
            {
                return new()
                {
                    Id = device.Id,
                    VIN = DevicePropertyHellper.getVIN(device),

                };
            }

            DeviceStatusInfo deviceStatus = resultsSearch[0];

            resultDevice = new()
            {
                Id = device.Id,
                VIN = DevicePropertyHellper.getVIN(device),
                Latitude = deviceStatus.Latitude,
                Longitude = deviceStatus.Longitude,
                TimeStamp = deviceStatus.DateTime,
            };
            await SearchGoDeviceStatusInfoAsyn(resultDevice);
            return resultDevice;
        }

        private async Task SearchGoDeviceStatusInfoAsyn(DeviceDto deviceDto)
        {
            var resultsSearch2 = await api.CallAsync<List<StatusData>>("Get", typeof(StatusData), new
            {
                search = new
                {
                    DeviceSearch = new DeviceSearch
                    {
                        Id = deviceDto.Id

                    },
                    diagnosticSearch = new
                    {
                        id = "DiagnosticOdometerId"
                    },
                    fromDate = deviceDto.TimeStamp.Value.AddDays(-1),
                    toDate = deviceDto.TimeStamp
                }
            });

            if (resultsSearch2.Count > 0)
            {
                deviceDto.Odometer = resultsSearch2.LastOrDefault(x => x.Data != null).Data;
            }
        }

        private void InitApi()
        {
            var apiSettings = this.configuration.GetSection("APISettings");
            APISettings aPISettings = new APISettings()
            {
                User = apiSettings["User"],
                Password = apiSettings["Password"],
                Database = apiSettings["Database"],
                Server = apiSettings["Server"]

            };
            this.api = new API(aPISettings.User, aPISettings.Password, null, aPISettings.Database, aPISettings.Server);
        }
    }
}
