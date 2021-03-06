using System;

namespace Levegominoseg
{
    class Program
    {
        static void Main(string[] args)
        {
            var downloader = new Downloader();

            var stationIds = new int[]
            {
                40, // Széna tér
                45, // Budapest Honvéd
                46, // Erzsébet tér
            };

            var data = downloader.Download(stationIds);
        }
    }
}
