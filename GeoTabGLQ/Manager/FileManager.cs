using GeoTabGLQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoTabGLQ.Manager
{
    public class FileManager()
    {
        public void WriteAllDeviceFile(string directoryPath, List<DeviceDto> devices)
        {
            Console.WriteLine("Writting Files ... #\r");

            foreach (DeviceDto device in devices)
            {
                WriteDeviceFile(directoryPath, device);

            }
        }
        public void WriteDeviceFile(string directoryPath, DeviceDto device)
        {
            try
            {
                string fileName = $"{device.Id}.csv";
                string filePath = Path.Combine(directoryPath, fileName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                bool fileExists = File.Exists(filePath);

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {

                    if (!fileExists)
                    {
                        writer.WriteLine("Id,TimeStamp,VIN,Coordinates,Odometer");
                    }
                    writer.WriteLine($"{device.Id},{device.TimeStamp},{device.VIN},{device.Coordinates.Replace(',', '.')},{device.Odometer}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error al escribir en el fichero CSV: " + e.Message);
            }
        }

    }
}
