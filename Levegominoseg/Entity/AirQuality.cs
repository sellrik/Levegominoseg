using System;
using System.Collections.Generic;
using System.Text;

namespace Levegominoseg.Entity
{
    public class AirQuality
    {
        public int StationId { get; set; }

        public string StationName { get; set; }

        public double? No { get; set; }

        public double? No2 { get; set; }

        public double? Nox { get; set; }

        public double? Co { get; set; }

        public double? Pm10 { get; set; }

        public double? Pm25 { get; set; }

        public DateTime DateOfLastMeasurement { get; set; }
    }
}
