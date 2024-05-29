// See https://aka.ms/new-console-template for more information
using Geotab.Checkmate;
using Geotab.Checkmate.ObjectModel;
using GeoTabGLQ.Manager;
using GeoTabGLQ.Models;
using Microsoft.Extensions.Configuration;

class Program
{


    static async Task Main(string[] args)
    {
        string PathString = "./";
        string Folder = "devices/";

        while (true)
        {
            var configuration = new ConfigurationBuilder()
                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .Build();

            DeviceManager deviceManager = new DeviceManager(configuration);
            FileManager fileManager = new FileManager();

            Console.WriteLine("Searching ... #\r");

            List<DeviceDto> devices = await deviceManager.SearchDevicesAsyn();

            Console.WriteLine("------------------------\n");
            Console.WriteLine(devices.Count() + " devices were found #\r");

            string filePath = PathString + Folder;
            fileManager.WriteAllDeviceFile(filePath, devices);

            Console.WriteLine("------------------------\n");
            Console.WriteLine("File writted in " + Path.GetFullPath(filePath));

            Console.WriteLine(" Completed#\r");

            await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }
}

