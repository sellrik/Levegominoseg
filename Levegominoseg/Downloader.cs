using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using Levegominoseg.Entity;
using Levegominoseg.Model;
using Newtonsoft.Json;

namespace Levegominoseg
{
    class Downloader
    {
        readonly int[] StationIds = new int[]
        {
            40, // Széna tér
            45, // Budapest Honvéd
            46, // Erzsébet tér
        };

        public void Download()
        {
            var result = new List<AirQuality>();

            foreach (var stationId in StationIds)
            {
                result.Add(DownloadStation(stationId));
                Thread.Sleep(3000);
            }
        }

        AirQuality DownloadStation(int stationId)
        {
            string response;
            using (var client = new HttpClient())
            {
                response = client.GetStringAsync($"http://www.levegominoseg.hu/olm/GetActualMeasuredComponentsValues?stationId={stationId}").Result;
            }

            var data = Parse(response);
            var aiqQuality = GetAirQuality(data);
            return aiqQuality;
        }

        ActualMeasuredComponentsValuesModel Parse(string response)
        {
            var tmp = new string(response);
            tmp = response
                .Replace("\\\"", "\"")
                .Replace("\"{", "{")
                .Replace("}\"", "}");

            var parsed = JsonConvert.DeserializeObject<ActualMeasuredComponentsValuesModel>(tmp);
            return parsed;
        }

        AirQuality GetAirQuality(ActualMeasuredComponentsValuesModel model)
        {
            return new AirQuality
            {
                StationId = model.StationInformation.SerialCode,
                StationName = model.StationInformation.StationName,
                No = model.GetMeasurement("NO"),
                No2 = model.GetMeasurement("NO2"),
                Nox = model.GetMeasurement("NOX"),
                Co = model.GetMeasurement("CO"),
                Pm10 = model.GetMeasurement("PM10"),
                Pm25 = model.GetMeasurement("PM2.5"),
                DateOfLastMeasurement = model.DateOfLastMeasurement
            };
        }
    }
}
