using Geotab.Checkmate.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoTabGLQ.Models
{
    public class DeviceDto
    {
         private string? vin;
    private double? odometer;
        public Geotab.Checkmate.ObjectModel.Id? Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string? VIN { get; set; }
        public string Coordinates => $"{Latitude} | {Longitude}";
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? Odometer { get; set; }
       
    }
}
