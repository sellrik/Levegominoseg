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
    public class Downloader
    {
        public List<AirQuality> Download(int[] stationIds, int sleepSec = 3)
        {
            var result = new List<AirQuality>();

            foreach (var stationId in stationIds)
            {
                try
                {
                    result.Add(DownloadStation(stationId));
                    throw new Exception();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Download failed. StationId: {stationId}, Exception: {ex.Message}");
                }
                finally
                {
                    Thread.Sleep(sleepSec * 1000);
                }
            }

            return result;
        }

        AirQuality DownloadStation(int stationId)
        {
            Console.WriteLine($"Downloading. Station: {stationId}");
            string response;
            using (var client = new HttpClient())
            {
                response = client.GetStringAsync($"http://www.levegominoseg.hu/olm/GetActualMeasuredComponentsValues?stationId={stationId}").Result;
            }

            Console.WriteLine($"Downloaded. Station: {stationId}");

            ActualMeasuredComponentsValuesModel data;

            try
            {
                Console.WriteLine($"Parsing data. Station: {stationId}");
                data = Parse(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse data. Station: {stationId}, Exception: {ex.ToString()}");
                throw;
            }

            AirQuality aiqQuality;
        
            try
            {
                Console.WriteLine($"Processing data. Station: {stationId}");
                aiqQuality = GetAirQuality(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process data. Station: {stationId}, Exception: {ex.ToString()}");
                throw;
            }

            Console.WriteLine($"Download finished. Station: {stationId}");

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
